using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_arrow : MonoBehaviour
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
        this.transform.position += Vector3.left * speed * Time.deltaTime;
       
    }

    void OnTriggerEnter2D(Collider2D col) {
        // check what this game object had collided with
        switch(col.gameObject.tag) {
            case "player_unit":
                // when adding hit sound effect
                // move bullet of screen
                // play the sound effect
                // Destory(this.gameobject, 10f), destroy after 10 sec
                if (col.gameObject.GetComponent<unit_1>() != null)
                {
                    Damage = 50;
                    col.gameObject.GetComponent<unit_1>().takeDamge(Damage);
                } else if (col.gameObject.GetComponent<unit_2>() != null)
                {
                    Damage = 100;
                    float damgeModifer = Damage * 0.85f;
                    col.gameObject.GetComponent<unit_2>().takeDamge(Damage + damgeModifer);

                } else if (col.gameObject.GetComponent<unit_3>() != null)
                {
                    Damage = 50;
                    float damgeModifer = Damage * 0.55f;
                    col.gameObject.GetComponent<unit_3>().takeDamge(Damage - damgeModifer);
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
