using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giant_fireball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    private Rigidbody2D bod;
    public float damage, timer, damageToPlayer;
    private AudioSource fireball;

    // Start is called before the first frame update
    void Start()
    {
        // at start play the bullet animation in loop
        // play bullet sound effect
        fireball = GetComponent<AudioSource>();
        fireball.Play();
        bod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime;       
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        // if bullet hits player's units it will deal damage.
        if (col.gameObject.tag == "player_unit")
        {
            if (col.gameObject.GetComponent<unit_1>() != null)
            {
                col.gameObject.GetComponent<unit_1>().takeDamge(damage);
              
            }

            else if (col.gameObject.GetComponent<unit_2>() != null)
            {
                col.gameObject.GetComponent<unit_2>().takeDamge(damage);
               
            }

            else if (col.gameObject.GetComponent<unit_3>() != null)
            {
                col.gameObject.GetComponent<unit_3>().takeDamge(damage);
                
            }
        }
        else if (col.gameObject.tag == "Player") {
            if (col.gameObject.GetComponent<player_1_movement>() != null)
            {
                col.gameObject.GetComponent<player_1_movement>().takeDamge(damageToPlayer);
                Destroy(this.gameObject);
            }
        }
        else 
        {
           Destroy(this.gameObject, 10f);
        }

    }
}

