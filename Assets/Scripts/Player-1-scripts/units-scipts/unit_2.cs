using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_2 : Unit_class
{
    // Start is called before the first frame update
 //   private Rigidbody2D bod;
    public GameObject healthBar;
    public GameObject armorBar;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D box;
    [SerializeField]private AudioSource spawnEffect, hurtEffect, attackEffect, deadEffect;
    private Rigidbody2D bod;
    private bool canMove, attacking;
    [SerializeField]private float timeBetweenAttacks;
    [SerializeField]private Transform attackPos;
    [SerializeField]private LayerMask enemies;
    [SerializeField]private float attackRangeX, attackRangeY;
    private float startTimeAttack, maxHp;
    private bool atSeventyfive, atFifty, atTwentyFive, isSheilded;

    private float armor;
    void Start()
    {
        startTimeAttack = timeBetweenAttacks;
        atSeventyfive = false;
        isSheilded = true;
        atFifty = false;
        atTwentyFive = false;
        bod = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        canMove = true;
        attacking = false;
        health = 370f;
        maxHp = health;
        damge = 70f;
        speed = 1.1f;
        armor = 300f;
        spawnEffect.pitch = Random.Range(1f, 1.4f);
        healthBar.GetComponent<HealthBarContoller>().InitializeHealthBar(health);
        armorBar.GetComponent<HealthBarContoller>().InitializeHealthBar(armor);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true) {
            move();
        }
        if (attacking == true) {
            attack(damge);
        }

    }
    public void healUnit(float amount) {
        health += amount;
        if (health > maxHp) {
            health = maxHp;
        }
        healthBar.GetComponent<HealthBarContoller>().updateHealthBar(health);

    }

    public override void attack(float damge)
    {
        //play attack animation
        if (timeBetweenAttacks <= 0) {
            anim.SetTrigger("isAttacking");
            attackEffect.pitch = Random.Range(0.8f, 1.2f);
            attackEffect.Play();
            // creates the box to detect enemies
            Collider2D []enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY), 0, enemies);
            for (int i = 0; i < enemiesToDamage.Length; i++){
                if (enemiesToDamage[i].gameObject.GetComponent<swordsman_ai>() != null)
                {
                    enemiesToDamage[i].gameObject.GetComponent<swordsman_ai>().takeDamge(damge);
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<archer_ai>() != null)
                {
                    enemiesToDamage[i].gameObject.GetComponent<archer_ai>().takeDamge(damge);
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<hammer_ai>() != null)
                {
                    float damageModifier = damge * .70f; // deal 70% extra damage to horsemen
                    enemiesToDamage[i].gameObject.GetComponent<hammer_ai>().takeDamge(damge + damageModifier);
                }
            }
            timeBetweenAttacks = startTimeAttack;
        } else {
            timeBetweenAttacks -= Time.deltaTime;
           // Debug.Log(timeBetweenAttacks);
        }
    }

    public override void dead()
    {
        //play death animation
        anim.SetTrigger("isDead");
        deadEffect.pitch = Random.Range(0.8f, 1.5f);
        deadEffect.Play();
        canMove = false;
        attacking = false;
        bod.simulated = false;
        box.enabled = false;
        healthBar.SetActive(false);
        armorBar.SetActive(false);
        Destroy(this.gameObject, 0.5f);
    }
    public override void move()
    {
        // play the walk animation

       bod.velocity = new Vector2(1 * speed, bod.velocity.y);
        //transform.position += Vector3.right.normalized * speed * Time.deltaTime;
    }
    public override void scan()
    {
        // might not even need this func
    }

    public void TakeDamgeHorsemen(float damage) {
        if (isSheilded == true) {
            armor -= damage;
            armorBar.GetComponent<HealthBarContoller>().updateHealthBar(armor);
             if(armor <= 0) {
                isSheilded = false;
            }
            return;
        }
        if (isSheilded == false) {
          health -= damage;
          if (health <= 0) {
            dead();
          }
        }
         
    }
    
    public override void takeDamge(float damge)
    {
        if (isSheilded == true) {
            armor -= damge;
            armorBar.GetComponent<HealthBarContoller>().updateHealthBar(armor);
            if(armor <= 0) {
                isSheilded = false;
            }
            return;
        }
        // play take damge animation
        if (isSheilded == false) {
            health -= damge;
            healthBar.GetComponent<HealthBarContoller>().updateHealthBar(health);
            if (health > 0) {
                hurtEffect.Play();
            anim.SetTrigger("isHurt");
            }
            else if (health <= 0) {
                dead();
            }
        }
        
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
       if (col.gameObject.CompareTag("enemy")) {
            bod.constraints= RigidbodyConstraints2D.FreezeAll;
           attacking = true;
           canMove = false;
       }
    }
    void OnCollisionExit2D(Collision2D col) {
        
        bod.constraints= RigidbodyConstraints2D.None;
        bod.constraints= RigidbodyConstraints2D.FreezePositionY;
        bod.constraints= RigidbodyConstraints2D.FreezeRotation;
        
        canMove = true;
        attacking = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX,attackRangeY,1));
    }
}
