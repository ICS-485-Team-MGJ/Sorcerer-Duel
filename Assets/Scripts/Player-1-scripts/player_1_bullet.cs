using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_1_bullet : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float _damage;
    private Rigidbody2D bod;
    [SerializeField]private AudioSource spawnEffect;


    // getter and setter for player's bullet
    public float Damage {
        get {
            return _damage;
        }
        set {
            _damage = value;
        }
    }

    // Start is called before the first frame update
    void Start() {
        // at start play the bullet animation in loop
        // play bullet sound effect
        bod = GetComponent<Rigidbody2D>();
        //spawnEffect.pitch = Random.Range(2.5f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * speed * Time.deltaTime;
       
    }

    void OnTriggerEnter2D(Collider2D col) {
        // check what this game object had collided with
        switch(col.gameObject.tag) {
            case "enemy":
                // when adding hit sound effect
                // move bullet of screen
                // play the sound effect
                // Destory(this.gameobject, 10f), destroy after 10 sec
                if (col.gameObject.GetComponent<swordsman_ai>() != null)
                {
                    col.gameObject.GetComponent<swordsman_ai>().takeDamge(Damage);
                }
                else if (col.gameObject.GetComponent<enemy_movement>() != null)
                {
                    col.gameObject.GetComponent<enemy_movement>().takeDamge(30);
                }
                else if (col.gameObject.GetComponent<archer_ai>() != null)
                {
                    col.gameObject.GetComponent<archer_ai>().takeDamge(Damage);
                }
                else if (col.gameObject.GetComponent<hammer_ai>() != null)
                {
                    col.gameObject.GetComponent<hammer_ai>().takeDamge(Damage);
                }
                 this.transform.position = new Vector3(100, 100, 100);
                Destroy(this.gameObject, 5f);
                break;
            default :
                // destroy the bullet otherwise after 10 seconds
                Destroy(this.gameObject, 10f);
                break;

        }

    }
}
