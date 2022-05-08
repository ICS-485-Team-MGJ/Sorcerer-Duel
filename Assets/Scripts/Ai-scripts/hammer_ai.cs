using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer_ai : Unit_class
{
    private BoxCollider2D box;
    private Animator anim;
    public GameObject HealthBar;
    private SpriteRenderer sprite;
    private Rigidbody2D bod;
    private bool canAttack, canMove, /*atSeventyfive, atFifty, atTwentyFive,*/ isCharging;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private float attackRangeX, attackRangeY;
    [SerializeField] private AudioSource spawned;
    private float myChargeTimer;
    private float startTimeAttack, defaultSpeed, chargeSpeed, maxHp;

    private int randomDamage;
    private float chargeDamage, extraDamgeModifier;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        extraDamgeModifier = 0.50f;
        startTimeAttack = timeBetweenAttacks;
        isCharging = false;
        box = GetComponent<BoxCollider2D>();
        bod = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        canMove = true;
        canAttack = false;
        health = 500;
        maxHp = health;
        damge = 150;
        speed = -0.9f;
        defaultSpeed = speed;
        chargeSpeed = -2.5f;
        chargeDamage = 2000;
        spawned.pitch = Random.Range(1f, 1.4f);
        myChargeTimer = 0;
        HealthBar.GetComponent<HealthBarContoller>().InitializeHealthBar(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (myChargeTimer >= 0.7f)
        {
            isCharging = true;
            myChargeTimer = 0;
        }
        if (canMove == true && canAttack == false)
        {
            myChargeTimer += 0.001f;
            move();
        }
        else if (canAttack == true && canMove == false)
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
            HealthBar.GetComponent<HealthBarContoller>().updateHealthBar(health);

        }
    */
    public override void attack(float damge)
    {
        // randomDamage = Mathf.FloorToInt(Random.Range(100, 150));
        anim.SetTrigger("isFighting");
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, enemies);
        if (isCharging == true)
        {
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].gameObject.GetComponent<unit_2>() != null)
                {
                    float modifiedChargeDamge = 250f; // charging swordsmen deals only 150 damage
                    float returnDamage = modifiedChargeDamge * 0.5f; // charging swordsmen reflects 50% of charge back to horsemen
                    enemiesToDamage[i].gameObject.GetComponent<unit_2>().TakeDamgeHorsemen(modifiedChargeDamge);
                    takeDamge(returnDamage); // reflect back the charge damage to the horsemen.
                    isCharging = false;

                    return;
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<unit_1>() != null)
                {

                    enemiesToDamage[i].gameObject.GetComponent<unit_1>().TakeDamgeHorsemen(9999);
                    isCharging = false;


                    return;
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<unit_3>() != null)
                {
                    // testing
                    enemiesToDamage[i].gameObject.GetComponent<unit_3>().TakeDamgeHorsemen(Mathf.FloorToInt(Random.Range(100, 500)));
                    isCharging = false;

                    return;
                }
            }
            return;

        }
        else
        {
            if (timeBetweenAttacks <= 0)
            {
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].gameObject.GetComponent<unit_2>() != null)
                    {
                        float damageModifier = damge * .70f; // deal 70% less damage to swordsmen
                        enemiesToDamage[i].gameObject.GetComponent<unit_2>().takeDamge(damge - damageModifier);

                    }
                    else if (enemiesToDamage[i].gameObject.GetComponent<unit_1>() != null)
                    {
                        float damageModifier = damge * .80f; // deal 80% less damage to archers
                        enemiesToDamage[i].gameObject.GetComponent<unit_1>().takeDamge(damge + damageModifier);

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
            }
        }
    }


    public override void dead()
    {
        anim.SetTrigger("isDead");
        canMove = false;
        canAttack = false;
        bod.simulated = false;
        box.enabled = false;
        HealthBar.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<player_1_movement>().Kill_Count += 1;// Marcos added this to count player unit kills
        Destroy(this.gameObject, 0.7f);
    }
    public override void move()
    {
        if (isCharging == true)
        {
            speed = chargeSpeed;
        }
        else
        {
            speed = defaultSpeed;
        }
        if (canAttack == false)
        {
            anim.SetTrigger("isWalking");
        }

        bod.velocity = new Vector2(speed, bod.velocity.y);
    }
    public override void takeDamge(float damge)
    {
        // play take damge animation
        health -= damge;
        if (health > 0)
        {
            anim.SetTrigger("isHit");
        }
        HealthBar.GetComponent<HealthBarContoller>().updateHealthBar(health);
        if (health <= 0)
        {
            dead();
        }

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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "player_unit")
        {
            bod.constraints = RigidbodyConstraints2D.FreezeAll;
            canAttack = true;
            canMove = false;
            myChargeTimer = 0;
        }
        else if (col.gameObject.tag == "enemy")
        {
            myChargeTimer = 0;
            isCharging = false;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {

        bod.constraints = RigidbodyConstraints2D.None;
        bod.constraints = RigidbodyConstraints2D.FreezePositionY;
        bod.constraints = RigidbodyConstraints2D.FreezeRotation;

        canMove = true;
        canAttack = false;
    }
    public override void scan()
    {
        // will not use now
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
