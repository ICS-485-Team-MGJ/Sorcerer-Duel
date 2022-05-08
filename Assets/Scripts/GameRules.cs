using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRules : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, enemyPlayer;
    private timer timer;
    private int finalplayerBarrierCount;
    private int finalAiBarrierCount;
    private int maxBarriers;
    private GameObject [] playerBarriers;
    private GameObject [] aiBarriers;
    void Start()
    {
        timer = GetComponent<timer>();
        playerBarriers = GameObject.FindGameObjectsWithTag("playerBarrier");
        aiBarriers = GameObject.FindGameObjectsWithTag("enemyBarrier");
        finalAiBarrierCount = 0;
        finalplayerBarrierCount = 0;
        maxBarriers = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.timerHasExpired() == false) {
             //RULE 1: if player or enemy player dies
            if (player.GetComponent<player_1_movement>().Health <= 0) {
                StartCoroutine(playerDead(0.5f));
                //UnityEditor.EditorApplication.isPlaying = false; // replace this to load you lose scene, later
            }
        
             else if (enemyPlayer.GetComponent<enemy_movement>().Health <= 0) {
                SceneManager.LoadScene("WinScene");
                //UnityEditor.EditorApplication.isPlaying = false; // replace this to load you win scene, later
            }
            // if the timer has not yet expire, return to save some cpu time
            return;
        }
       
        // RULE 2: Time EXpires
        if (timer.timerHasExpired() == true) {
            // checks the number of barriers left for each player, and counts them
            for (int i = 0; i < maxBarriers; i++) {
                if (playerBarriers[i].GetComponent<SpriteRenderer>().enabled == true) {
                    finalplayerBarrierCount++;
                }
                if (aiBarriers[i].GetComponent<SpriteRenderer>().enabled == true) {
                    finalAiBarrierCount++;
                }
            }

            //check for greater number of barriers

            // if player has more barrier than ai, player win
            if (finalplayerBarrierCount > finalAiBarrierCount) {
                SceneManager.LoadScene("WinScene");
                //UnityEditor.EditorApplication.isPlaying = false; // replace this to load you win scene, later

            // if ai has more barrier than player, player lose
            } else if (finalplayerBarrierCount < finalAiBarrierCount) {
                SceneManager.LoadScene("LoseScene");
                //UnityEditor.EditorApplication.isPlaying = false; // replace this to load you win scene, later

            }
            // if both player has the same number of barrier standing
            else if (finalplayerBarrierCount == finalAiBarrierCount) {
                // check who has the most hp
                // later on implement sudden death, unit spawning 2x, units faster, bullets for both are also faster

                // if player has more hp, player win
                if (player.GetComponent<player_1_movement>().Health > enemyPlayer.GetComponent<enemy_movement>().Health) {
                    SceneManager.LoadScene("WinScene");
                    //UnityEditor.EditorApplication.isPlaying = false; // replace this to load you win scene, later
                } // if player has less hp, player lose
                else if (player.GetComponent<player_1_movement>().Health < enemyPlayer.GetComponent<enemy_movement>().Health) {
                    SceneManager.LoadScene("LoseScene");
                    //UnityEditor.EditorApplication.isPlaying = false; // replace this to load you win scene, later
                }
            }
           
        }

        
    }

    IEnumerator playerDead(float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LoseScene");

    }
}
