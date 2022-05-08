using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_1_spell_1 : MonoBehaviour
{
    private GameObject target;
    private float speed = 12.1f;
    private float meteorDamage = 95f;
    private Rigidbody2D bod;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(target != null) {
            this.transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void setTarget(GameObject obj) {
        target = obj;
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name.CompareTo(target.gameObject.name) == 0) {
            if (col.gameObject.GetComponent<swordsman_ai>() != null)
            {
                float modifier = meteorDamage * 0.5f;
                float hit = meteorDamage + modifier;
                col.gameObject.GetComponent<swordsman_ai>().takeDamge(hit);
            }
            else if (col.gameObject.GetComponent<archer_ai>() != null)
            {
                float modifier = meteorDamage * 0.75f;
                float hit = meteorDamage + modifier;
                col.gameObject.GetComponent<archer_ai>().takeDamge(hit);
            }
            else if (col.gameObject.GetComponent<hammer_ai>() != null)
            {
                float modifier = meteorDamage * 0.25f;
                float hit = meteorDamage + modifier;
                col.gameObject.GetComponent<hammer_ai>().takeDamge(hit);
            }
            Destroy(this.gameObject);
        } else {
            Destroy(this.gameObject, 5f);
        }
    }
}
