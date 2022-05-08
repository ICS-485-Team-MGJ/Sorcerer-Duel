using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_laser : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter (Collider col) {
        if (col.gameObject.GetComponent<swordsman_ai>() != null)
        {
            col.gameObject.GetComponent<swordsman_ai>().takeDamge(9999);
        }
        if (col.gameObject.GetComponent<archer_ai>() != null)
        {
            col.gameObject.GetComponent<archer_ai>().takeDamge(9999);
        }
        if (col.gameObject.GetComponent<hammer_ai>() != null)
        {
            col.gameObject.GetComponent<hammer_ai>().takeDamge(9999);
        }
    }
  
}
