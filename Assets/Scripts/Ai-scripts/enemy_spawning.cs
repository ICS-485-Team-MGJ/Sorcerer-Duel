using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawning : MonoBehaviour
{
    public GameObject[] spawns;
    public LayerMask obj;
    private Collider2D enemiesToDamage;
    public GameObject enemy_unit;
    public GameObject enemy_unit2;
    public GameObject enemy_unit3;
    private float maxDistance, offset, timePassed;

    private int enemySelect;

    // public int maxSpawns;
    // private int counter = 0;
    private int slotNum, counter;
    public Vector2 decisionTime = new Vector2(1, 3);
    internal float decisionTimeCount = 0;
    private bool broken, twoPassed;
    [SerializeField] private float width, height;

    void Start()
    {
        offset = 0;
        broken = false;
        decisionTimeCount = 0;
        timePassed = 0;
        twoPassed = false;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        increaseSpawnRate();
        enemySelect = Mathf.FloorToInt(Random.Range((float)(0.75), 3));
        slotNum = Mathf.FloorToInt(Random.Range(0, spawns.Length));
        if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;
        else
        {
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);


            switch (spawns[slotNum].gameObject.name)
            {
                case "enemy_spawn-0":
                    Collider2D enemiesToDamage = Physics2D.OverlapBox(spawns[slotNum].transform.position, new Vector2(width, height), 0, obj);
                    if (enemiesToDamage != null)
                    {
                        return;
                    }
                    // randomize the spawning of the three units.
                    if (enemySelect == 0)
                    {
                        Instantiate(enemy_unit, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                    }
                    else if (enemySelect == 1)
                    {
                        Instantiate(enemy_unit2, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);

                    }
                    else
                    {
                        if (broken || twoPassed)
                        {
                            Instantiate(enemy_unit3, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    break;
                case "enemy_spawn-1":
                    enemiesToDamage = Physics2D.OverlapBox(spawns[slotNum].transform.position, new Vector2(width, height), 0, obj);
                    if (enemiesToDamage != null)
                    {
                        return;
                    }
                    // randomize the spawning of the three units.
                    if (enemySelect == 0)
                    {
                        Instantiate(enemy_unit, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                    }
                    else if (enemySelect == 1)
                    {
                        Instantiate(enemy_unit2, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);

                    }
                    else
                    {
                        if (broken || twoPassed)
                        {
                            Instantiate(enemy_unit3, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    break;
                case "enemy_spawn-2":
                    enemiesToDamage = Physics2D.OverlapBox(spawns[slotNum].transform.position, new Vector2(width, height), 0, obj);
                    if (enemiesToDamage != null)
                    {
                        return;
                    }
                    // randomize the spawning of the three units.
                    if (enemySelect == 0)
                    {
                        Instantiate(enemy_unit, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                    }
                    else if (enemySelect == 1)
                    {
                        Instantiate(enemy_unit2, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);

                    }
                    else
                    {
                        if (broken || twoPassed)
                        {
                            Instantiate(enemy_unit3, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    break;
                case "enemy_spawn-3":
                    enemiesToDamage = Physics2D.OverlapBox(spawns[slotNum].transform.position, new Vector2(width, height), 0, obj);
                    if (enemiesToDamage != null)
                    {
                        return;
                    }
                    // randomize the spawning of the three units.
                    if (enemySelect == 0)
                    {
                        Instantiate(enemy_unit, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                    }
                    else if (enemySelect == 1)
                    {
                        Instantiate(enemy_unit2, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);

                    }
                    else
                    {
                        if (broken || twoPassed)
                        {
                            Instantiate(enemy_unit3, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    break;
                case "enemy_spawn-4":
                    enemiesToDamage = Physics2D.OverlapBox(spawns[slotNum].transform.position, new Vector2(width, height), 0, obj);
                    if (enemiesToDamage != null)
                    {
                        return;
                    }
                    // randomize the spawning of the three units.
                    if (enemySelect == 0)
                    {
                        Instantiate(enemy_unit, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                    }
                    else if (enemySelect == 1)
                    {
                        Instantiate(enemy_unit2, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);

                    }
                    else
                    {
                        if (broken || twoPassed)
                        {
                            Instantiate(enemy_unit3, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    break;
                case "enemy_spawn-5":
                    enemiesToDamage = Physics2D.OverlapBox(spawns[slotNum].transform.position, new Vector2(width, height), 0, obj);
                    if (enemiesToDamage != null)
                    {
                        return;
                    }
                    // randomize the spawning of the three units.
                    if (enemySelect == 0)
                    {
                        Instantiate(enemy_unit, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                    }
                    else if (enemySelect == 1)
                    {
                        Instantiate(enemy_unit2, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);

                    }
                    else
                    {
                        if (broken || twoPassed)
                        {
                            Instantiate(enemy_unit3, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    break;
                case "enemy_spawn-6":
                    enemiesToDamage = Physics2D.OverlapBox(spawns[slotNum].transform.position, new Vector2(width, height), 0, obj);
                    if (enemiesToDamage != null)
                    {
                        return;
                    }
                    // randomize the spawning of the three units.
                    if (enemySelect == 0)
                    {
                        Instantiate(enemy_unit, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                    }
                    else if (enemySelect == 1)
                    {
                        Instantiate(enemy_unit2, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);

                    }
                    else
                    {
                        if (broken || twoPassed)
                        {
                            Instantiate(enemy_unit3, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    break;
                case "enemy_spawn-7":
                    enemiesToDamage = Physics2D.OverlapBox(spawns[slotNum].transform.position, new Vector2(width, height), 0, obj);
                    if (enemiesToDamage != null)
                    {
                        return;
                    }
                    // randomize the spawning of the three units.
                    if (enemySelect == 0)
                    {
                        Instantiate(enemy_unit, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                    }
                    else if (enemySelect == 1)
                    {
                        Instantiate(enemy_unit2, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);

                    }
                    else
                    {
                        if (broken || twoPassed)
                        {
                            Instantiate(enemy_unit3, new Vector3(spawns[slotNum].transform.position.x - offset, spawns[slotNum].transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    break;

                default:
                    break;
            }
        }



        // Not sure this is going to work. Maybe?
        for (counter = 0; counter < spawns.Length; counter++)
        {
            if (spawns[counter].GetComponent<SpriteRenderer>().enabled == false)
            {
                broken = true;
            }
        }
    }

    private void increaseSpawnRate()
    {
        if (timePassed >= 180)
        {
            decisionTime = new Vector2((float)(0.5), (float)(0.5));
        }
        if (timePassed >= 120)
        {
            decisionTime = new Vector2((float)(0.65), 1);
            twoPassed = true;
        }
        if (timePassed >= 60)
        {
            decisionTime = new Vector2(1, 2);
        }
    }

        void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawns[0].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawns[1].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawns[2].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawns[3].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawns[4].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawns[5].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawns[6].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawns[7].gameObject.transform.position, new Vector3(width, height,1));
        
    }

}
