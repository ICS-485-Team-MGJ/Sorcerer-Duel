using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana_power_up : MonoBehaviour
{
    
    private float Mana_Amount;
    private SpriteRenderer sprite;
    public GameObject nextPowerUp;
    private float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        delayTime = Random.Range(17.5f, 40.5f);
        Mana_Amount = Random.Range(35f, 65f);
        sprite = GetComponent<SpriteRenderer>();
        // start the animation have it loop
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("player_unit")) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<player_1_movement>().regainMana(Mana_Amount);
            StartCoroutine(getNextPowerUp(delayTime));
        }
        else if (col.gameObject.CompareTag("enemy")) {
            StartCoroutine(getNextPowerUp(delayTime));

        }
    }

    IEnumerator getNextPowerUp(float delay) {
        sprite.enabled = false;
        yield return new WaitForSeconds(delay);
        Instantiate(nextPowerUp, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }
    
}
