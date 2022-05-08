using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_power_up : MonoBehaviour
{
    [SerializeField]private float Heal_Amout;
    private SpriteRenderer sprite;
    public GameObject nextPowerUp;
    private float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        delayTime = Random.Range(17.5f, 40.5f);
        sprite = GetComponent<SpriteRenderer>();
        // start the animation have it loop
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("player_unit")) {
            if (col.gameObject.GetComponent<unit_1>() != null) {
                col.gameObject.GetComponent<unit_1>().healUnit(Heal_Amout * .65f);
            }
            else if (col.gameObject.GetComponent<unit_2>() != null) {
                col.gameObject.GetComponent<unit_2>().healUnit(Heal_Amout * .65f);
            }
            else if (col.gameObject.GetComponent<unit_3>() != null) {
                col.gameObject.GetComponent<unit_3>().healUnit(Heal_Amout * .65f);
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<player_1_movement>().healPlayer(Heal_Amout * 0.35f);
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
