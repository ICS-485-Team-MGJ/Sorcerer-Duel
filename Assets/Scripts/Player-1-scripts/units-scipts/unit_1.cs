using UnityEngine;

public class unit_1 : Unit_class
{
    // Start is called before the first frame update
  // private Rigidbody2D bod;
    private SpriteRenderer sprite;
    private Rigidbody2D bod;
    private BoxCollider2D box;
    private Animator anim;
    [SerializeField]private AudioSource spawnEffect;
    public GameObject healthBar;

    private bool canMove, attacking;
    [SerializeField]private float timeBetweenAttacks;
    [SerializeField]private Transform attackPos, attackRange;
    [SerializeField]private LayerMask enemies;
    [SerializeField]private float attackRangeX, rangeAttackX, attackRangeY,rangeAttackY;
    [SerializeField]private GameObject arrows;
    private float startTimeAttack;
    private float lastShot, rangeAttackRate, maxHp;
    private bool inMelee;
    private bool atSeventyfive, atFifty, atTwentyFive;
    void Start()
    {
        rangeAttackRate = 1.1f;
        atSeventyfive = false;
        atFifty = false;
        atTwentyFive = false;
        inMelee = false;
        // temp = 100f;
        startTimeAttack = timeBetweenAttacks;
        lastShot = 0;
         bod = GetComponent<Rigidbody2D>();
         anim= GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        canMove = true; 
        attacking = false;
        health = 75f;
        maxHp = health;
        damge = 20f;
        speed = 1.1f;
        spawnEffect.pitch = Random.Range(0.8f, 1.2f);
        healthBar.GetComponent<HealthBarContoller>().InitializeHealthBar(health);

    }

    public void healUnit(float amount) {
        health += amount;
        if (health > maxHp) {
            health = maxHp;
        }
        healthBar.GetComponent<HealthBarContoller>().updateHealthBar(health);

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D []enemiesToDamage = Physics2D.OverlapBoxAll(attackRange.position, new Vector2(rangeAttackX, rangeAttackY), 0, enemies);
        for (int i = 0; i < enemiesToDamage.Length; i++){
            if (enemiesToDamage[i].gameObject.GetComponent<swordsman_ai>() != null && inMelee == false)
            {
                 if (Time.time > rangeAttackRate + lastShot) {
                    anim.SetTrigger("canShootArrow");
                    // this arrow gameObject had its damage modified to deal extra damage to swordsmen units only
                    Instantiate(arrows, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, 0), Quaternion.identity);
                    lastShot = Time.time;
                 } 
            }
            else if (enemiesToDamage[i].gameObject.GetComponent<archer_ai>() != null && inMelee == false)
            {
                 if (Time.time > rangeAttackRate + lastShot) {
                    anim.SetTrigger("canShootArrow");
                    Instantiate(arrows, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, 0), Quaternion.identity);
                    lastShot = Time.time;
                 } 
            }
             else if (enemiesToDamage[i].gameObject.GetComponent<hammer_ai>() != null && inMelee == false)
            {
                 if (Time.time > rangeAttackRate + lastShot) {                    
                     anim.SetTrigger("canShootArrow");

                    Instantiate(arrows, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, 0), Quaternion.identity);
                    lastShot = Time.time;
                 } 
            }
            }

        if (canMove == true) {
            move();
        }
        if (attacking == true) {
            attack(damge);
        }

    }
   public void TakeDamgeHorsemen(float damage) {
         health -= damage;
          if (health <= 0) {
            dead();
        }
    }

    public override void attack(float damge)
    {
        //play attack animation
        if (timeBetweenAttacks <= 0) {
            // creates the box to detect enemies
            Collider2D []enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY), 0, enemies);
            for (int i = 0; i < enemiesToDamage.Length; i++) {
                if (enemiesToDamage[i].gameObject.GetComponent<swordsman_ai>() != null)
                {
                    anim.SetTrigger("isInMelee");
                    enemiesToDamage[i].gameObject.GetComponent<swordsman_ai>().takeDamge(damge);
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<archer_ai>() != null)
                {            
                    anim.SetTrigger("isInMelee");
                    enemiesToDamage[i].gameObject.GetComponent<archer_ai>().takeDamge(damge);
                }
                else if (enemiesToDamage[i].gameObject.GetComponent<hammer_ai>() != null)
                {            
                    anim.SetTrigger("isInMelee");
                    enemiesToDamage[i].gameObject.GetComponent<hammer_ai>().takeDamge(damge);
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
        canMove = false;
        attacking = false;
        bod.simulated = false;
        box.enabled = false;
        healthBar.SetActive(false);
        Destroy(this.gameObject, 0.4f);
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
    public override void takeDamge(float damge)
    {
        // play take damge animation
        health -= damge;
        float currentHp = health;
        
        healthBar.GetComponent<HealthBarContoller>().updateHealthBar(currentHp);
        if (health > 0) {
            anim.SetTrigger("isHurt");
        }
        else if (health <= 0) {
            dead();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {           
       if (col.gameObject.tag == "enemy") {
            bod.constraints= RigidbodyConstraints2D.FreezeAll;
           attacking = true;
           inMelee = true;
           canMove = false;
       }
    }
    void OnCollisionExit2D(Collision2D col) {
        bod.constraints= RigidbodyConstraints2D.None;
        bod.constraints= RigidbodyConstraints2D.FreezePositionY;
       bod.constraints= RigidbodyConstraints2D.FreezeRotation;
        canMove = true;
        inMelee = false;
        attacking = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX,attackRangeY,1));
         Gizmos.color = Color.blue;
          Gizmos.DrawWireCube(attackRange.position, new Vector3(rangeAttackX,rangeAttackY,1));
    }
}
