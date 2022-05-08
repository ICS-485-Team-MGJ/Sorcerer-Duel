using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordsman_ai : Unit_class
{
    private Animator anim;
    private float armor;
    private BoxCollider2D box;
    public GameObject armorBar;
    public GameObject healthBar;
    private Rigidbody2D bod;
    private SpriteRenderer sprite; // Marcos Added this line
    private bool canMove, attacking;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private AudioSource spawned;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private float attackRangeX, attackRangeY;
    private float startTimeAttack, deadTimer, maxHp;
    private bool isSheilded;
    // private bool atSeventyfive, atFifty, atTwentyFive;

    // Start is called before the first frame update
    void Start()
    {
        isSheilded = true;
        anim = GetComponent<Animator>();
        startTimeAttack = timeBetweenAttacks;
        bod = GetComponent<Rigidbody2D>(); // Marcos Added this line
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        canMove = true;
        attacking = false;
        health = 370f;
        damge = 70f;
        speed = -1.1f;
        maxHp = health;
        spawned.pitch = Random.Range(0.8f, 1f);
        armor = 300f;
        healthBar.GetComponent<HealthBarContoller>().InitializeHealthBar(health);
        armorBar.GetComponent<HealthBarContoller>().InitializeHealthBar(armor);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            // walking.Play();
            move();
        }
        if (attacking == true)
        {
            attack(damge);
        }
    }

    public override void attack(float damge)
    {
        //play attack animation
        anim.SetBool("isAttacking", true);
        if (timeBetweenAttacks <= 0)
        {
            // creates the box to detect enemies
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, enemies);
            // Debug.Log("Attacking!");
            //enemiesToDamage.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //enemiesToDamage.gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));

            /* Added to detect the 2 different player summonunit scripts*/
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].gameObject.GetComponent<unit_1>() != null)
                {
                    enemiesToDamage[i].GetComponent<unit_1>().takeDamge(damge);
                }
                if (enemiesToDamage[i].gameObject.GetComponent<unit_2>() != null)
                {
                    enemiesToDamage[i].gameObject.GetComponent<unit_2>().takeDamge(damge);
                }
                if (enemiesToDamage[i].gameObject.GetComponent<unit_3>() != null)
                {
                    float damageModifier = damge * .70f; // deal 70% extra damage to horsemen

                    enemiesToDamage[i].gameObject.GetComponent<unit_3>().takeDamge(damge + damageModifier);
                }

            }


            /*
            temp -= damge;
            if (temp <= 0)
            {
                // Destroy(enemiesToDamage.gameObject);
                temp = 100f;
            }
            */

            timeBetweenAttacks = startTimeAttack;
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
            // Debug.Log(timeBetweenAttacks);
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

    // method for unit death
    public override void dead()
    {
        anim.SetTrigger("isDead");
        canMove = false;
        attacking = false;
        bod.simulated = false;
        box.enabled = false;
        healthBar.SetActive(false);
        armorBar.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<player_1_movement>().Kill_Count += 1;// Marcos added this to count player unit kills
        Destroy(this.gameObject, 0.7f);
    }

    // method for unit movement
    public override void move()
    {
        // play the walk animation
        // anim.ResetTrigger("isAttacking");
        anim.SetBool("isAttacking", false);
        bod.velocity = new Vector2(1 * speed, bod.velocity.y);
    }

    // method for unit scan
    public override void scan()
    {
        // might not even need this func
    }
    // Marcos added this for taking horsemen damage
    public void TakeDamgeHorsemen(float damage)
    {
        if (isSheilded == true)
        {
            armor -= damage;
            armorBar.GetComponent<HealthBarContoller>().updateHealthBar(armor);
            if (armor <= 0)
            {
                isSheilded = false;
            }
            return;
        }
        if (isSheilded == false)
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

    }

    // method for unit to take damage
    public override void takeDamge(float damage)
    {
        if (isSheilded == true)
        {
            armor -= damage;
            armorBar.GetComponent<HealthBarContoller>().updateHealthBar(armor);
            if (armor <= 0)
            {
                isSheilded = false;
            }
            return;
        }
        // play take damge animation
        if (isSheilded == false)
        {
            health -= damage;
            if (health > 0)
            {
                anim.SetTrigger("isHit");
            }
            healthBar.GetComponent<HealthBarContoller>().updateHealthBar(health);
            if (health <= 0)
            {
                dead();
            }
        }


    }

    // method for collision detection
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "player_unit")
        {
            bod.constraints = RigidbodyConstraints2D.FreezeAll;
            attacking = true;
            canMove = false;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        bod.bodyType = RigidbodyType2D.Dynamic;
        bod.constraints = RigidbodyConstraints2D.None;
        bod.constraints = RigidbodyConstraints2D.FreezePositionY;
        bod.constraints = RigidbodyConstraints2D.FreezeRotation;
        canMove = true;
        attacking = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
