using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archer_ai : Unit_class
{
    // Start is called before the first frame update
    private BoxCollider2D box;
    private SpriteRenderer sprite;
    private Rigidbody2D bod;
    [SerializeField] private AudioSource spawnEffect;
    public GameObject healthBar;
    private bool canMove, attacking;

    private Animator anim;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Transform attackPos, attackRange;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private float attackRangeX, rangeAttackX, attackRangeY, rangeAttackY;
    [SerializeField] private GameObject arrows;
    private float startTimeAttack;
    private float lastShot, rangeAttackRate, maxHp;
    private bool inMelee;
    private bool atSeventyfive, atFifty, atTwentyFive;
    void Start()
    {
        anim = GetComponent<Animator>();
        rangeAttackRate = 1.2f;
        atSeventyfive = false;
        atFifty = false;
        atTwentyFive = false;
        inMelee = false;
        maxHp = health;
        box = GetComponent<BoxCollider2D>();
        startTimeAttack = timeBetweenAttacks;
        lastShot = 0;
        bod = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        canMove = true;
        attacking = false;
        health = 60f;
        damge = 20f;
        speed = -1.1f;
        spawnEffect.pitch = Random.Range(1.2f, 1.4f);
        healthBar.GetComponent<HealthBarContoller>().InitializeHealthBar(health);

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackRange.position, new Vector2(rangeAttackX, rangeAttackY), 0, enemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].gameObject.GetComponent<unit_1>() != null && inMelee == false)
            {
                if (Time.time > rangeAttackRate + lastShot)
                {
                    anim.SetTrigger("isShooting");
                    Instantiate(arrows, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, 0), Quaternion.identity);
                    lastShot = Time.time;
                }
            }
            else if (enemiesToDamage[i].gameObject.GetComponent<unit_2>() != null && inMelee == false)
            {
                if (Time.time > rangeAttackRate + lastShot)
                {
                    anim.SetTrigger("isShooting");
                    Instantiate(arrows, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, 0), Quaternion.identity);
                    lastShot = Time.time;
                }
            }
            else if (enemiesToDamage[i].gameObject.GetComponent<unit_3>() != null && inMelee == false)
            {
                if (Time.time > rangeAttackRate + lastShot)
                {
                    anim.SetTrigger("isShooting");
                    Instantiate(arrows, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, 0), Quaternion.identity);
                    lastShot = Time.time;
                }
            }
        }

        if (canMove == true)
        {
            move();
        }
        if (attacking == true)
        {
            attack(damge);
        }

    }

    /*
        public void healUnit(float amount) {
            health += amount;
            if (health > maxHp) {
                health = maxHp;
            }
            healthBar.GetComponent<HealthBarContoller>().updateHealthBar(health);

        }
    */
    public override void attack(float damge)
    {
        //play attack animation
        anim.SetTrigger("isMelee");
        if (timeBetweenAttacks <= 0)
        {
            // creates the box to detect enemies
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, enemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].gameObject.GetComponent<unit_1>() != null)
                {
                    enemiesToDamage[i].gameObject.GetComponent<unit_1>().takeDamge(damge);
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<unit_2>() != null)
                {
                    enemiesToDamage[i].gameObject.GetComponent<unit_2>().takeDamge(damge);
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<unit_3>() != null)
                {
                    enemiesToDamage[i].gameObject.GetComponent<unit_3>().takeDamge(damge);
                }
            }

            timeBetweenAttacks = startTimeAttack;
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
            // Debug.Log(timeBetweenAttacks);
        }
    }

    public override void dead()
    {
        //play death animation
        anim.SetTrigger("isDead");
        canMove = false;
        attacking = false;
        bod.simulated = false;
        box.enabled = false;
        healthBar.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<player_1_movement>().Kill_Count += 1;// Marcos added this to count player unit kills
        Destroy(this.gameObject, 0.7f);
    }
    public override void move()
    {
        // play the walk animation
        bod.velocity = new Vector2(1 * speed, bod.velocity.y);
        // transform.position += Vector3.right.normalized * speed * Time.deltaTime;

    }
    public override void scan()
    {
        // might not even need this func
    }
    public void TakeDamgeHorsemen(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            anim.SetTrigger("isHit");
        }
        if (health <= 0)
        {
            dead();
        }
    }
    public override void takeDamge(float damge)
    {
        // play take damge animation
        health -= damge;
        float currentHp = health;
        if (health > 0)
        {
            anim.SetTrigger("isHit");
        }
        //sprite.color = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F)); // this is just a temp take damge animation
        //sprite.color = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F)); // this is just a temp take damge animation
        healthBar.GetComponent<HealthBarContoller>().updateHealthBar(currentHp);

        if (health <= 0)
        {
            dead();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        // attacking = true;
        //  canMove = false;

        if (col.gameObject.tag == "player_unit")
        {
            bod.constraints = RigidbodyConstraints2D.FreezeAll;
            attacking = true;
            inMelee = true;
            canMove = false;
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        bod.constraints = RigidbodyConstraints2D.None;
        bod.constraints = RigidbodyConstraints2D.FreezePositionY;
        bod.constraints = RigidbodyConstraints2D.FreezeRotation;
        canMove = true;
        inMelee = false;
        attacking = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackRange.position, new Vector3(rangeAttackX, rangeAttackY, 1));
    }
}
