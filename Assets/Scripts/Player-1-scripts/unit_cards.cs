using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_cards : MonoBehaviour
{
    public GameObject cards;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i ++) {
            GameObject unitsCards = Instantiate(cards, new Vector3(0, 0, 0), Quaternion.identity);
            unitsCards.transform.SetParent(this.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
