using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_2_bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    private Rigidbody2D bod;
    public float damage;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        // at start play the bullet animation in loop
        // play bullet sound effect
        bod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime;
        if (this.transform.position.x <= maxDistance)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "player_unit")
        {
            if (col.gameObject.GetComponent<unit_1>() != null)
            {
                col.gameObject.GetComponent<unit_1>().takeDamge(damage);
                Destroy(this.gameObject);
            }

            if (col.gameObject.GetComponent<unit_2>() != null)
            {
                col.gameObject.GetComponent<unit_2>().takeDamge(damage);
                Destroy(this.gameObject);
            }
        }

        if (col.gameObject.tag == "player_1_barrier")
        {
            Destroy(this.gameObject);
        }
    }
}
