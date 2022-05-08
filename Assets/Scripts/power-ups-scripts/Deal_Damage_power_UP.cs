using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Damage_power_UP : MonoBehaviour
{
    
    private float damage;
    private SpriteRenderer sprite;
    public GameObject nextPowerUp;
    public GameObject target;
    private float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        delayTime = Random.Range(27.5f, 40.5f);
        damage = 150f;
        sprite = GetComponent<SpriteRenderer>();
        // start the animation have it loop
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("player_unit")) {
            GameObject.Find("enemy ai").GetComponent<enemy_movement>().takeDamge(damage);
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
