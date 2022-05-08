using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy_movement : MonoBehaviour
{
    internal Transform thisTransform;
    private Animator anim;
    private SpriteRenderer sprite;
    public GameObject healthBar;
    [SerializeField] private float maxChanneling, channelingRate, maxHp;
    public GameObject bullet, fireAttack, AudioManager;
    [SerializeField] private float _health;
    private float channelingSuperBullet, fullHealth;
    private bool canShoot, fireBall, moveFaster, goCrazy, startMusic;
    // private int[] numOfBullets = { 1, 2, 3 };
    private float num, sevenFive, fifty, twoFive;
    Camera m_MainCamera;
    public AudioSource hurt;
    public AudioSource main;
    public AudioSource intense;


    // The movement speed of the object
    public float moveSpeed;

    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new Vector2(0, 1);
    internal float decisionTimeCount = 0;

    // The possible directions that the object can move int, right, left, up, down, and zero for staying in place. I added zero twice to give a bigger chance if it happening than other directions
    internal Vector3[] moveDirections = new Vector3[] { Vector3.up.normalized, Vector3.down.normalized, Vector3.zero };
    internal int currentMoveDirection;

    // get and seter for enemy
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        startMusic = false;
        fullHealth = _health;
        sevenFive = (float)(fullHealth * 0.75);
        fifty = (float)(fullHealth * 0.5);
        twoFive = (float)(fullHealth * 0.25);
        fireBall = false;
        moveFaster = false;
        goCrazy = false;
        hurt = GetComponent<AudioSource>();
        m_MainCamera = Camera.main;
        maxHp = Health;
        main = m_MainCamera.GetComponent<AudioSource>();
        intense = AudioManager.GetComponent<AudioSource>();
        healthBar.GetComponent<HealthBarContoller>().InitializeHealthBar(Health);

        sprite = GetComponent<SpriteRenderer>();

        canShoot = true;
        channelingSuperBullet = 0f;
        // Cache the transform for quicker access
        thisTransform = this.transform;

        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = 1;

        // have AI stay still when game starts. To fix bug
        currentMoveDirection = 2;

        // Choose a movement direction, or stay in place
    }

    // Update is called once per frame
    void Update()
    {

        // Move the object in the chosen direction at the set speed
        thisTransform.position += moveDirections[currentMoveDirection] * Time.deltaTime * moveSpeed;

        if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = 3;

            if (canShoot)
            {
                Instantiate(bullet, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, 0), Quaternion.identity);
            }
            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }

        if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;
        else
        {
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

            if (fireBall)
            {
                Instantiate(fireAttack, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, 0), Quaternion.identity);
            }
            else if (moveFaster)
            {
                moveSpeed = 7;
            }

        }

    }

    public void healEnemy(float amount)
    {
        Debug.Log("Heal " + amount);
        Health += amount;
        if (Health > maxHp)
        {
            Health = maxHp;
        }
        healthBar.GetComponent<HealthBarContoller>().updateHealthBar(Health);

    }

    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (currentMoveDirection == 0)
        {
            currentMoveDirection = 1;
        }
        else
        {
            currentMoveDirection = 0;
        }
    }

    public void takeDamge(float damage)
    {
        anim.SetTrigger("isHit");
        _health -= damage;
        healthBar.GetComponent<HealthBarContoller>().updateHealthBar(Health);
        hurt.Play();
        // sprite.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F)); // change color when attack to indicate damage
        if (Health <= 0)
        {
            // this.transform.position = new Vector3(100,100,0);
            // this.GetComponent<enemy_movement>().enabled = false;
            dead();
        }
        /*
        if (Health <= sevenFive) {
            moveFaster = true;
        }
        */
        if (Health <= fifty)
        {
            fireBall = true;
            main.Stop();
            if (startMusic == false)
            {
                startMusic = true;
                intense.Play();
            }
        }
        if (Health == twoFive)
        {
            goCrazy = true;
            fireBall = true;
        }

    }

    private void dead()
    {
        anim.SetTrigger("isDead");
        StartCoroutine(enemyDead(0.5f));
    }

    IEnumerator enemyDead(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.GetComponent<enemy_movement>().enabled = false;
    }
}

