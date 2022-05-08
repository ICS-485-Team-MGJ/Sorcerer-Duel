using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_barrier : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite seventyFive, fifty, twentyFive;

    [SerializeField] private float hp;
    public GameObject player2;
    private SpriteRenderer sprite;
    private bool atSeventyfive, atFifty, atTwentyFive;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        atSeventyfive = false;
        atFifty = false;
        atTwentyFive = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // if the barrier hp is greater than 0
        if ((col.tag == "player_unit") && sprite.enabled == true)
        {
            takeDamge(100);
            Destroy(col.gameObject);
        }
        // if barrier hp less than or equal to 0
        else if (col.tag == "player_unit" && sprite.enabled == false)
        {
            Destroy(col.gameObject);
            if (player2 != null)
            {
                player2.GetComponent<enemy_movement>().takeDamge(250); 
            }
        }
    }

    public void takeDamge(float damge)
    {
        hp -= damge;
        float currentHp = hp;
        //sprite.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F)); // this is just a temp take damge animation
        if (hp <= (currentHp * 0.75) && atSeventyfive == false) {
            sprite.sprite = seventyFive;
            atSeventyfive = true;
            // sprite.color = new Color(0, 1, 0, 1);
             currentHp = hp;
        }
        else if (hp <= (currentHp* 0.5) && atFifty == false) {
            sprite.sprite = fifty;
            atFifty = true;
            // sprite.color = new Color(0.5f, 0.5f, 0.5f, 1);
             currentHp = hp;
        } else if (hp <= (currentHp * 0.25) && atTwentyFive == false) {
            sprite.sprite = twentyFive;
            atTwentyFive = true;
            // sprite.color = new Color(1, 0, 0, 1);
             currentHp = hp;
        }
        else if (hp <= 0 && atSeventyfive == true && atFifty == true && atTwentyFive == true) {
           sprite.enabled = false;
        }
       
    }
}
