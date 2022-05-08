using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Player_Spell_controller : MonoBehaviour
{
    public GameObject[] spellCards, lasersInLane;
    public TextMeshProUGUI text, spellWarningText;
    private Collider2D[] enemiesToDamage, enemiesInLaser;
    public LayerMask enemies;
    public GameObject spellArea, laser, spell_1, player, playerManaBar;
    private bool canCastSpell_1, canCastSpell_2, canCastSpell_3;
    [SerializeField] private float cooldDownSP1, cooldDownSP2, cooldDownSP3;
    [SerializeField] private float rangeSpellWidth, rangeSpellHieght, laserWidth, lasetHieght;
    private int waves;
    private float startingMana, currentMana;

    private AudioSource spellTwo, spellThree;
    // Start is called before the first frame update
    void Start()
    {
        canCastSpell_1 = true;
        canCastSpell_2 = true;
        canCastSpell_3 = true;
        waves = 4;
        player.GetComponent<player_1_movement>().Mana = 100;
        player.GetComponent<player_1_movement>().playerInvincible = false;
        startingMana = player.GetComponent<player_1_movement>().Mana;
        playerManaBar.GetComponent<HealthBarContoller>().InitializeHealthBar(startingMana);
        player.GetComponent<player_1_movement>().playerCanShoot = true;
        player.GetComponent<player_1_movement>().playerCanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = (int)player.GetComponent<player_1_movement>().Mana + " / " + startingMana;
        if (Input.GetKeyDown(KeyCode.Alpha1) && player.GetComponent<player_1_movement>().Mana >= 40 && canCastSpell_1 == true)
        {
            enemiesToDamage = Physics2D.OverlapBoxAll(spellArea.transform.position, new Vector2(rangeSpellWidth, rangeSpellHieght), 0, enemies);
            if (enemiesToDamage.Length >= 1)
            {
                canCastSpell_1 = false;
                spellCards[0].GetComponent<Image>().color = Color.gray;
                spellCards[0].GetComponent<Image>().fillAmount = 0;
                player.GetComponent<player_1_movement>().Mana -= 40;
                if (player.GetComponent<player_1_movement>().Mana < 0)
                {
                    player.GetComponent<player_1_movement>().Mana = 0;
                }
                float remainingMana = player.GetComponent<player_1_movement>().Mana;
                playerManaBar.GetComponent<HealthBarContoller>().updateHealthBar(remainingMana);
                for (int x = 0; x < waves; x++)
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].gameObject != null)
                        {
                            GameObject meteors = Instantiate(spell_1, new Vector3(-10.0f, 10f, 0), Quaternion.identity);
                            meteors.GetComponent<player_1_spell_1>().setTarget(enemiesToDamage[i].gameObject);
                        }
                    }
                }
                StartCoroutine(resetSpellOne(cooldDownSP1));
            }
            else
            {
                spellWarningText.enabled = true;
                spellWarningText.text = "Unable to cast Spell, No enemies Found!";
                StartCoroutine(disableMessage(1.5f));
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && player.GetComponent<player_1_movement>().Mana < 40 && canCastSpell_1 == true)
        {
            spellWarningText.enabled = true;
            spellWarningText.text = "Not Enough Mana to Cast Spell 1!";
            StartCoroutine(disableMessage(1.5f));

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && canCastSpell_2 == true && player.GetComponent<player_1_movement>().Mana >= 60)
        {
            spellTwo = laser.GetComponent<AudioSource>();
            spellTwo.Play();
            enemiesInLaser = Physics2D.OverlapBoxAll(laser.transform.position, new Vector2(laserWidth, lasetHieght), 0, enemies);
            player.GetComponent<player_1_movement>().playerCanShoot = false;
            player.GetComponent<player_1_movement>().playerCanMove = false;
            canCastSpell_2 = false;
            spellCards[1].GetComponent<Image>().color = Color.gray;
            spellCards[1].GetComponent<Image>().fillAmount = 0;
            player.GetComponent<player_1_movement>().Mana -= 60f;
            if (player.GetComponent<player_1_movement>().Mana < 0)
            {
                player.GetComponent<player_1_movement>().Mana = 0;
            }
            float remainingMana = player.GetComponent<player_1_movement>().Mana;
            playerManaBar.GetComponent<HealthBarContoller>().updateHealthBar(remainingMana);
            StartCoroutine(lasers(0.5f));
            for (int i = 0; i < enemiesInLaser.Length; i++)
            {
                if (enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>() != null)
                {
                    enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>().takeDamge(9999);
                }
                else if (enemiesInLaser[i].gameObject.GetComponent<archer_ai>() != null)
                {
                    enemiesInLaser[i].gameObject.GetComponent<archer_ai>().takeDamge(9999);
                }
                else if (enemiesInLaser[i].gameObject.GetComponent<hammer_ai>() != null)
                {
                    enemiesInLaser[i].gameObject.GetComponent<hammer_ai>().takeDamge(9999);
                }
            }
            StartCoroutine(resetSpellTwo(cooldDownSP2));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canCastSpell_2 == true && player.GetComponent<player_1_movement>().Mana < 60)
        {
            spellWarningText.enabled = true;
            spellWarningText.text = "Not Enough Mana to Cast Spell 2!";
            StartCoroutine(disableMessage(1.5f));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && canCastSpell_3 == true && player.GetComponent<player_1_movement>().Mana >= 100)
        {
            player.GetComponent<player_1_movement>().playerInvincible = true;
            canCastSpell_3 = false;
            spellCards[2].GetComponent<Image>().color = Color.gray;
            spellCards[2].GetComponent<Image>().fillAmount = 0;
            player.GetComponent<player_1_movement>().Mana -= 100f;
            if (player.GetComponent<player_1_movement>().Mana < 0)
            {
                player.GetComponent<player_1_movement>().Mana = 0;
            }
            float remainingMana = player.GetComponent<player_1_movement>().Mana;
            playerManaBar.GetComponent<HealthBarContoller>().updateHealthBar(remainingMana);
            StartCoroutine(laserPatterns(0.5f));


        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canCastSpell_3 == true && player.GetComponent<player_1_movement>().Mana < 100)
        {
            spellWarningText.enabled = true;
            spellWarningText.text = "Not Enough Mana to Cast Spell 3!";
            StartCoroutine(disableMessage(1.5f));
        }


        currentMana = player.GetComponent<player_1_movement>().Mana;
        if (currentMana >= 40 && canCastSpell_1 == true)
        {
            spellCards[0].GetComponent<Image>().color = Color.white;
        }
        else
        {
            spellCards[0].GetComponent<Image>().color = Color.gray;
        }

        if (currentMana >= 60 && canCastSpell_2 == true)
        {
            spellCards[1].GetComponent<Image>().color = Color.white;
        }
        else
        {
            spellCards[1].GetComponent<Image>().color = Color.gray;
        }

        if (currentMana >= 100 && canCastSpell_3 == true)
        {
            spellCards[2].GetComponent<Image>().color = Color.white;
        }
        else
        {
            spellCards[2].GetComponent<Image>().color = Color.gray;

        }


    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spellArea.transform.position, new Vector3(rangeSpellWidth, rangeSpellHieght, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(laser.transform.position, new Vector3(laserWidth, lasetHieght, 1));
        Gizmos.color = Color.green;
        for (int i = 0; i < lasersInLane.Length; i++)
        {
            Gizmos.DrawWireCube(lasersInLane[i].gameObject.transform.position, new Vector3(laserWidth, lasetHieght, 1));
        }


    }
    IEnumerator lasers(float timer)
    {
        laser.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(timer);
        laser.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<player_1_movement>().playerCanShoot = true;
        player.GetComponent<player_1_movement>().playerCanMove = true;
    }

    IEnumerator disableMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        spellWarningText.enabled = false;

    }

    IEnumerator allLasers(float timer, GameObject obj)
    {
        spellThree = lasersInLane[0].GetComponent<AudioSource>();
        spellThree.Play();
        obj.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(timer);
        obj.GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator playerIsInvisible(float timer)
    {
        yield return new WaitForSeconds(timer);
        player.GetComponent<player_1_movement>().playerInvincible = false;

    }

    IEnumerator laserPatterns(float timer)
    {
        // pattern 1
        for (int x = 0; x < lasersInLane.Length; x++)
        {
            StartCoroutine(allLasers(0.5f, lasersInLane[x].gameObject));
            enemiesInLaser = Physics2D.OverlapBoxAll(lasersInLane[x].transform.position, new Vector2(laserWidth, lasetHieght), 0, enemies);
            for (int i = 0; i < enemiesInLaser.Length; i++)
            {
                if (enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>() != null)
                {
                    enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>().takeDamge(9999);
                }
                else if (enemiesInLaser[i].gameObject.GetComponent<archer_ai>() != null)
                {
                    enemiesInLaser[i].gameObject.GetComponent<archer_ai>().takeDamge(9999);
                }
                else if (enemiesInLaser[i].gameObject.GetComponent<hammer_ai>() != null)
                {
                    enemiesInLaser[i].gameObject.GetComponent<hammer_ai>().takeDamge(9999);
                }
            }
            yield return new WaitForSeconds(timer);
        }

        // pattern 2
        for (int x = 0; x < lasersInLane.Length; x++)
        {
            if (x % 2 == 0)
            {
                StartCoroutine(allLasers(0.5f, lasersInLane[x].gameObject));
                enemiesInLaser = Physics2D.OverlapBoxAll(lasersInLane[x].transform.position, new Vector2(laserWidth, lasetHieght), 0, enemies);
                for (int i = 0; i < enemiesInLaser.Length; i++)
                {
                    if (enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>() != null)
                    {
                        enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>().takeDamge(9999);
                    }
                    else if (enemiesInLaser[i].gameObject.GetComponent<archer_ai>() != null)
                    {
                        enemiesInLaser[i].gameObject.GetComponent<archer_ai>().takeDamge(9999);
                    }
                    else if (enemiesInLaser[i].gameObject.GetComponent<hammer_ai>() != null)
                    {
                        enemiesInLaser[i].gameObject.GetComponent<hammer_ai>().takeDamge(9999);
                    }
                }
                yield return new WaitForSeconds(0.3f);
            }
            else if (x % 2 != 0)
            {
                StartCoroutine(allLasers(0.5f, lasersInLane[x].gameObject));
                enemiesInLaser = Physics2D.OverlapBoxAll(lasersInLane[x].transform.position, new Vector2(laserWidth, lasetHieght), 0, enemies);
                for (int i = 0; i < enemiesInLaser.Length; i++)
                {
                    if (enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>() != null)
                    {
                        enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>().takeDamge(9999);
                    }
                    else if (enemiesInLaser[i].gameObject.GetComponent<archer_ai>() != null)
                    {
                        enemiesInLaser[i].gameObject.GetComponent<archer_ai>().takeDamge(9999);
                    }
                    else if (enemiesInLaser[i].gameObject.GetComponent<hammer_ai>() != null)
                    {
                        enemiesInLaser[i].gameObject.GetComponent<hammer_ai>().takeDamge(9999);
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
        // pattern 3
        for (int x = 0; x < lasersInLane.Length; x++)
        {
            StartCoroutine(allLasers(0.5f, lasersInLane[x].gameObject));
            enemiesInLaser = Physics2D.OverlapBoxAll(lasersInLane[x].transform.position, new Vector2(laserWidth, lasetHieght), 0, enemies);
            for (int i = 0; i < enemiesInLaser.Length; i++)
            {
                if (enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>() != null)
                {
                    enemiesInLaser[i].gameObject.GetComponent<swordsman_ai>().takeDamge(9999);
                }
                else if (enemiesInLaser[i].gameObject.GetComponent<archer_ai>() != null)
                {
                    enemiesInLaser[i].gameObject.GetComponent<archer_ai>().takeDamge(9999);
                }
                else if (enemiesInLaser[i].gameObject.GetComponent<hammer_ai>() != null)
                {
                    enemiesInLaser[i].gameObject.GetComponent<hammer_ai>().takeDamge(9999);
                }
            }
        }



        StartCoroutine(resetSpellThree(cooldDownSP3));
        StartCoroutine(playerIsInvisible(15f));
    }


    IEnumerator resetSpellOne(float timer)
    {
        float test = 0;
        while (spellCards[0].GetComponent<Image>().fillAmount != 1)
        {
            yield return new WaitForSeconds(timer);
            spellCards[0].GetComponent<Image>().fillAmount = test;
            test += 0.01f;
        }

        //spellCards[0].GetComponent<Image>().color = Color.yellow;
        canCastSpell_1 = true;
    }

    IEnumerator resetSpellTwo(float timer)
    {
        float test = 0;
        while (spellCards[1].GetComponent<Image>().fillAmount != 1)
        {
            yield return new WaitForSeconds(timer);
            spellCards[1].GetComponent<Image>().fillAmount = test;
            test += 0.01f;
        }
        //spellCards[1].GetComponent<Image>().color = Color.yellow;
        canCastSpell_2 = true;
    }

    IEnumerator resetSpellThree(float timer)
    {
        float test = 0;
        while (spellCards[2].GetComponent<Image>().fillAmount != 1)
        {
            yield return new WaitForSeconds(timer);
            spellCards[2].GetComponent<Image>().fillAmount = test;
            test += 0.01f;
        }
        //spellCards[2].GetComponent<Image>().color = Color.yellow;
        canCastSpell_3 = true;
    }
}
