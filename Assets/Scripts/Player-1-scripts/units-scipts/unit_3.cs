using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_3 : Unit_class
{
    public GameObject HealthBar;
    public AudioSource soundAttack, soundDead, SoundHurt;
    private SpriteRenderer sprite;
    private BoxCollider2D box;
    private Rigidbody2D bod;
    private Animator anim;
    private bool canAttack, canMove, atSeventyfive, atFifty, atTwentyFive, isCharging;
    [SerializeField]private float timeBetweenAttacks;
    [SerializeField]private Transform attackPos;
    [SerializeField]private LayerMask enemies;
    [SerializeField]private float attackRangeX, attackRangeY;
    private float myChargeTimer;
    private float startTimeAttack, defaultSpeed, chargeSpeed, maxHp;
    private float chargeDamage, extraDamgeModifier;
    // Start is called before the first frame update
    void Start()
    {
        extraDamgeModifier = 0.50f;
        startTimeAttack = timeBetweenAttacks;
        atSeventyfive = false;
        isCharging = false;
        atFifty = false;
        atTwentyFive = false;
        bod = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        canMove = true;
        canAttack = false;
        health = 500;
        maxHp = health;
        damge = 150;
        speed = 0.9f;
        defaultSpeed = speed;
        chargeSpeed = 2.5f;
        chargeDamage = 2000;
        myChargeTimer = 0;
        HealthBar.GetComponent<HealthBarContoller>().InitializeHealthBar(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (myChargeTimer >= 0.7f) {
            isCharging = true; 
            myChargeTimer = 0;
        }
        if (canMove == true && canAttack == false) {
            myChargeTimer += 0.001f;
            move();
        }else if (canAttack == true && canMove == false) {
            attack(damge);
        }
        
    }
    public void healUnit(float amount) {
        health += amount;
        if (health > maxHp) {
            health = maxHp;
        }
        HealthBar.GetComponent<HealthBarContoller>().updateHealthBar(health);

    }

    public override void attack(float damge)
    {
        Collider2D []enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY), 0, enemies);
        if (isCharging == true) {
             for (int i = 0; i < enemiesToDamage.Length; i++) {
                if (enemiesToDamage[i].gameObject.GetComponent<swordsman_ai>() != null)
                {
                    float modifiedChargeDamge = 250f; // charging swordsmen deals only 150 damage
                    float returnDamage = modifiedChargeDamge * 0.5f; // charging swordsmen reflects 50% of charge back to horsemen
                    enemiesToDamage[i].gameObject.GetComponent<swordsman_ai>().TakeDamgeHorsemen(modifiedChargeDamge);
                    takeDamge(returnDamage); // reflect back the charge damage to the horsemen.
                    break;
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<archer_ai>() != null)
                {
                    enemiesToDamage[i].gameObject.GetComponent<archer_ai>().TakeDamgeHorsemen(9999);
                    break;
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<hammer_ai>() != null)
                {
                    enemiesToDamage[i].gameObject.GetComponent<hammer_ai>().TakeDamgeHorsemen(9999);
                    break;
                }
            }
            isCharging = false;
            return;

        } else {
            if (timeBetweenAttacks <= 0) {
                anim.SetTrigger("isAttacking");
                soundAttack.pitch = Random.Range(0.8f, 1.2f);
                soundAttack.Play();
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    if (enemiesToDamage[i].gameObject.GetComponent<swordsman_ai>() != null)
                    {
                        float damageModifier = damge * .70f; // deal 70% less damage to swordsmen
                        enemiesToDamage[i].gameObject.GetComponent<swordsman_ai>().takeDamge(damge - damageModifier);
                    }
                    else if (enemiesToDamage[i].gameObject.GetComponent<archer_ai>() != null)
                    {
                        float damageModifier = damge * .80f; // deal 80% less damage to archers
                        enemiesToDamage[i].gameObject.GetComponent<archer_ai>().takeDamge(damge + damageModifier);
                    }
                    else if (enemiesToDamage[i].gameObject.GetComponent<hammer_ai>() != null)
                    {
                        enemiesToDamage[i].gameObject.GetComponent<hammer_ai>().takeDamge(damge);
                    }
                }
                timeBetweenAttacks = startTimeAttack;
        } else {
            timeBetweenAttacks -= Time.deltaTime;
        }
        }
    }


    public override void dead()
    {
        anim.SetTrigger("isDead");
        soundDead.pitch = Random.Range(0.8f, 1.2f);
        soundDead.Play();
        canMove = false;
        canAttack = false;
        bod.simulated = false;
        box.enabled = false;
        HealthBar.SetActive(false);
        Destroy(this.gameObject, 0.7f);
    }
    public override void move()
    {
        if (isCharging == true) {
            speed = chargeSpeed;
        }
        else {
            speed = defaultSpeed;
        }
        bod.velocity = new Vector2(speed, bod.velocity.y);
    }
    public override void takeDamge(float damge)
    {
        health -= damge;
        HealthBar.GetComponent<HealthBarContoller>().updateHealthBar(health);
        if (health > 0) {
            anim.SetTrigger("isHurt");
            SoundHurt.pitch = Random.Range(0.8f, 1.2f);
            SoundHurt.Play();
        }
        else if (health <= 0) {
            dead();
        }
        
    }

    public void TakeDamgeHorsemen(float damages) {
         health -= damages;
          if (health <= 0) {
            dead();
        }
    }

 void OnCollisionEnter2D(Collision2D col)
    {
        
       if (col.gameObject.CompareTag("enemy")) {
           bod.constraints= RigidbodyConstraints2D.FreezeAll;
           canAttack = true;
           canMove = false;
           myChargeTimer = 0;
       }
       else if (col.gameObject.CompareTag("player_unit")) {
           myChargeTimer = 0;
           isCharging = false;
       }
    }
    void OnCollisionExit2D(Collision2D col) {
        
        bod.constraints= RigidbodyConstraints2D.None;
        bod.constraints= RigidbodyConstraints2D.FreezePositionY;
        bod.constraints= RigidbodyConstraints2D.FreezeRotation;
        
        canMove = true;
        canAttack = false;
    }
    public override void scan()
    {
        // will not use now
    }


      void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX,attackRangeY,1));
    }
}
