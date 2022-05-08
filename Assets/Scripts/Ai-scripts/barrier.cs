using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float hp;
    public GameObject player;
    [SerializeField]private Sprite twentyFive, fifty, seventyFive;
    private SpriteRenderer sprite;
    private bool atSeventyfive, atFifty, atTwentyFive, canSummonHere;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        atSeventyfive = false;
        atFifty = false;
        atTwentyFive = false;
    }

    public bool SummonHere {
        get {
            return canSummonHere;
        }
        set {
            canSummonHere = value;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        // if the barrier hp is greater than 0
        if (col.CompareTag("enemy") && sprite.enabled == true) {
            takeDamge(100); // Glen changed the damage taken to 100
            Destroy(col.gameObject);
        }
        // if barrier hp lessthan or equal to 0
        else if (col.CompareTag("enemy") && sprite.enabled == false) {
            Destroy(col.gameObject);
            if (player != null)
            {
                 player.GetComponent<player_1_movement>().takeDamge(250);
            }
        }
    }

    public void takeDamge(float damge) {
        hp -= damge;
        float currentHp = hp;
        //sprite.color = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F)); // this is just a temp take damge animation
        if (hp <= (currentHp * 0.75) && atSeventyfive == false) {
            atSeventyfive = true;
            sprite.sprite = seventyFive;
        }
        else if (hp <= (currentHp* 0.5) && atFifty == false) {
            atFifty = true;
            sprite.sprite = fifty;
        } else if (hp <= (currentHp * 0.25) && atTwentyFive == false) {
            atTwentyFive = true;
            sprite.sprite = twentyFive;
        }
        else if (hp <= 0 && atSeventyfive == true && atFifty == true && atTwentyFive == true) {
            sprite.enabled = false;
        }
    }
}
