using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_1_movement : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Collider2D []enemiesToDamage;
    public GameObject spellArea, spell_1;
    private Animator anim;
    public GameObject healthBar, manaBar;
    [SerializeField]private AudioSource playerHurt;
    [SerializeField] private float speed, maxChanneling, channelingRate, rateOfFire, maxHp, maxMana;
    private float _health, mana;
    public GameObject bullet, superBullet;
    private float channelingSuperBullet, lastShot;
    [SerializeField]private LayerMask enemies;
    private bool canMove, invincible;
    private bool canShoot;

    private int killCount, waves;
    

    public int Kill_Count {
        get {
            return killCount;
        }
        set {
            killCount = value;
            
        }
    }

    public bool playerInvincible {
        get {
            return invincible;
        }
        set {
            invincible = value;
        }
    }

    public bool playerCanMove {
        get {
            return canMove;
        }
        set {
            canMove = value;
        }
    }

    public bool playerCanShoot {
        get {
            return canShoot;
        }
        set {
            canShoot = value;
        }
    }

    public float Mana {
        get {
            return mana;
        }
        set {
            mana = value;
        }
    }
    
    // get and seter for player
    public float Health {
        get {
            return _health;
        }
        set {
            _health = value;
        }
    }

    // Update is called once per frame
    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        lastShot = 0;
        Kill_Count = 0;
        waves = 4;
        Health = 1000f;
        maxHp = Health;
        maxMana = 100;
        channelingSuperBullet = 0f;
        healthBar.GetComponent<HealthBarContoller>().InitializeHealthBar(Health);
    }

    public void healPlayer(float amount) {
        Health += amount;
        if (Health > maxHp) {
            Health = maxHp;
        }
        healthBar.GetComponent<HealthBarContoller>().updateHealthBar(Health);
    }

    public void regainMana(float amount) {
        Mana += amount;
        if (Mana > maxMana) {
            Mana = maxMana;
        }
        manaBar.GetComponent<HealthBarContoller>().updateHealthBar(Mana);

    }
    
    void Update()
    {
        StartCoroutine(refillMana(0.5f));
        //Debug.Log("Kill Count: " + killCount);
        if (invincible) {
            sprite.color = Color.yellow;
        }
        if (!invincible) {
            sprite.color = Color.white;
        }
        if (Input.GetKey(KeyCode.S) && playerCanMove == true) {
           transform.position += Vector3.down.normalized * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) && playerCanMove == true) {
           transform.position += Vector3.up.normalized * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) & playerCanShoot == true) {
          shoot();
        }

        // this is for channeling a super projectile.
        if (Input.GetKey(KeyCode.C)) {
            // player will be unable to move and shoot but can still summon while channeling
            canMove = false;
            canShoot = false;
            channelingSuperBullet += channelingRate;
        }
        if (Input.GetKeyUp(KeyCode.C)){
            if (channelingSuperBullet >= maxChanneling) {
                Instantiate(superBullet, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, 0), Quaternion.identity);
            }
            canMove = true;
            canShoot = true;
            channelingSuperBullet = 0f;
        }
    }

    void shoot() {
        // controls the amount of time a playr can shoot
        if (Time.time > rateOfFire + lastShot) {
            Instantiate(bullet, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, 0), Quaternion.identity);
            lastShot = Time.time;
        }
    }

    public void takeDamge(float damage) {
        if (playerInvincible == false) {
            _health -= damage;
            healthBar.GetComponent<HealthBarContoller>().updateHealthBar(Health);
            if (Health <= 0) {
                dead();
            } else {
                playerHurt.pitch = Random.Range(1, 2);
                playerHurt.Play();
                anim.SetTrigger("isHurt");
            }        
        }
    }

    private void dead() {
        anim.SetTrigger("isDead");
        StartCoroutine(playerDead(0.2f));
    }

    IEnumerator playerDead(float delay) {
        yield return new WaitForSeconds(delay);
        this.GetComponent<player_1_movement>().enabled = false;
    }
    IEnumerator refillMana(float delay) {
        if (Mana < 100) {
            Mana += 0.01f;
            manaBar.GetComponent<HealthBarContoller>().updateHealthBar(Mana);
            yield return new WaitForSeconds(delay);
        } 
    }

     
}
