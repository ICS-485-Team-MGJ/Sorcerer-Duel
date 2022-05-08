using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_PowerUps : MonoBehaviour
{
    public GameObject[] powerUpsSpanwLocation;
    private int maxPowerUps, index, counter;
    // Start is called before the first frame update
    void Start()
    {
        maxPowerUps = 4;
        counter = 0;
        while(counter < maxPowerUps) {
            index = Random.Range(0, powerUpsSpanwLocation.Length - 1);
            if (powerUpsSpanwLocation[index].activeInHierarchy == false) {
                powerUpsSpanwLocation[index].SetActive(true);
                counter++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        counter = GameObject.FindGameObjectsWithTag("power-up").Length;
        if (counter < maxPowerUps) {
            for (int i = 0; i < powerUpsSpanwLocation.Length; i++) {
                if (i % 2 == 0) {
                    Instantiate(powerUps[0], new Vector3(powerUpsSpanwLocation[i].transform.position.x, powerUpsSpanwLocation[i].transform.position.y, powerUpsSpanwLocation[i].transform.position.z), Quaternion.identity);
                }
            }
            // create another array to store the index used, to prevent using the index that is already
        }
        */
    }
}
