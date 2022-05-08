using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spawning : MonoBehaviour
{
        public GameObject []spanws;
        public TextMeshProUGUI text, text_2;
        public Camera cam;
        public AudioSource rally;
        public LayerMask obj;
        private Collider2D enemiesToDamage;
        [SerializeField]private float archerSpawnCooldown, swordsMenSpawnCooldown, knightSpawnCooldown;
        [SerializeField]private int maxKill;
        public GameObject []units, unitCards;
        private float maxDistance, offset;
        private float cooldDown;
        private int index;
        private Color selected, notSelected;
        private int maxUnits, unitCounter;
        private bool canSummon, canSpecialSummon;
        private bool canSummonArcher, canSummonSwordsmen, canSummonKnight;
        [SerializeField]private float width, height;

   void Start() {
      canSummon = true;
      canSpecialSummon = true;
      canSummonArcher = true;
      canSummonSwordsmen = true;
      canSummonKnight = true;
      cooldDown = 0;
      maxUnits = 16;
      unitCounter = 0;
      offset = 0;
      index = 0;
      selected = Color.yellow;
      notSelected = Color.grey;
      for (int x = 0; x < spanws.Length; x++) {
         spanws[x].GetComponent<barrier>().SummonHere = true;
      }
   }
   

    // Update is called once per frame
    void Update()
    {
       unitCounter = GameObject.FindGameObjectsWithTag("player_unit").Length;
       text_2.text = unitCounter + " / " + maxUnits;
       if (unitCounter < maxKill) {
         text.text = GameObject.FindGameObjectWithTag("Player").GetComponent<player_1_movement>().Kill_Count + " / " + maxKill;
       }
       // special move
       if (GameObject.FindGameObjectWithTag("Player").GetComponent<player_1_movement>().Kill_Count >= maxKill) {
          //Debug.Log("");
          //text_2.enabled = true;
          if (canSpecialSummon) {
            text.text = "Press Q to RAllY!!";
          } else if (!canSpecialSummon) {
            text.text = "Rallying!!";
          }
          GameObject.FindGameObjectWithTag("Player").GetComponent<player_1_movement>().Kill_Count = maxKill;
          if (Input.GetKeyDown(KeyCode.Q)  && canSpecialSummon == true) {
             canSpecialSummon = false;
             rally.pitch = Random.Range(0.8f, 1.4f);
             rally.Play();
             StartCoroutine(SpecialAbiltiy(10));
          }
          
       }

       // switching between units.
       if (Input.GetKeyDown(KeyCode.A)) {
          index--;
          if (index <= -1) {
             index = 0;
          }
       }
      
       if (Input.GetKeyDown(KeyCode.D)) {
          index++;
          if (index >= units.Length) {
             index = units.Length - 1;
          }
       }
       for (int i = 0; i < unitCards.Length; i++) {
      
         if (units[index].gameObject.name.CompareTo(unitCards[i].name) == 0) {
            unitCards[i].GetComponent<Image>().color = selected;
         } else {
            unitCards[i].GetComponent<Image>().color = notSelected;
         }
       }
      


        if (unitCounter < maxUnits) {
           if (Input.GetMouseButtonDown(0)) {
              Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
              Vector2 convertMousePos2D = new Vector2(mousePos.x, mousePos.y);

              RaycastHit2D hit = Physics2D.Raycast(convertMousePos2D, Vector2.zero);
              string clickedObj = "";
              if (hit.collider != null) {
                 clickedObj = hit.collider.gameObject.name;
              }
              switch(clickedObj) {
                 case "spawn-0":   
                  Collider2D enemiesToDamage = Physics2D.OverlapBox(spanws[0].transform.position, new Vector2(width, height), 0, obj);
                  if (enemiesToDamage != null) {
                     return;
                  }        
                     if (units[index].name == "Archer" && canSummonArcher == true) {
                        Instantiate(units[index], new Vector3(spanws[0].transform.position.x + offset, spanws[0].transform.position.y, 0), Quaternion.identity);
                        cooldDown = archerSpawnCooldown;
                        canSummon = false;
                        canSummonArcher = false;
                        unitCards[0].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummon(cooldDown));
                     }
                     else if (units[index].name == "Swordsmen" && canSummonSwordsmen == true) {
                        Instantiate(units[index], new Vector3(spanws[0].transform.position.x + offset, spanws[0].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonSwordsmen = false;
                        cooldDown = swordsMenSpawnCooldown;
                        unitCards[1].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonSwordsmen(cooldDown));
                     }
                     else if (units[index].name == "Knight" && canSummonKnight == true) {
                        Instantiate(units[index], new Vector3(spanws[0].transform.position.x + offset, spanws[0].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonKnight = false;
                        cooldDown = knightSpawnCooldown;
                        unitCards[2].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonKnight(cooldDown));
                     }
                     break;

                 case "spawn-1":
                     enemiesToDamage = Physics2D.OverlapBox(spanws[1].transform.position, new Vector2(width, height), 0, obj);
                     if (enemiesToDamage != null) {
                           return;
                     } 
                     if (units[index].name == "Archer" && canSummonArcher == true) {
                        Instantiate(units[index], new Vector3(spanws[1].transform.position.x + offset, spanws[1].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonArcher = false;
                        cooldDown = archerSpawnCooldown;
                        unitCards[0].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummon(cooldDown));
                     }
                     else if (units[index].name == "Swordsmen" && canSummonSwordsmen == true) {
                        Instantiate(units[index], new Vector3(spanws[1].transform.position.x + offset, spanws[1].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonSwordsmen = false;
                        cooldDown = swordsMenSpawnCooldown;
                        unitCards[1].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonSwordsmen(cooldDown));
                     }
                     else if (units[index].name == "Knight" && canSummonKnight == true) {
                        Instantiate(units[index], new Vector3(spanws[1].transform.position.x + offset, spanws[1].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonKnight = false;
                        cooldDown = knightSpawnCooldown;
                        unitCards[2].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonKnight(cooldDown));
                     }
                     break;

                 case "spawn-2":
                     enemiesToDamage = Physics2D.OverlapBox(spanws[2].transform.position, new Vector2(width, height), 0, obj);
                     if (enemiesToDamage != null) {
                           return;
                     }
                     if (units[index].name == "Archer" && canSummonArcher == true) {
                        Instantiate(units[index], new Vector3(spanws[2].transform.position.x + offset, spanws[2].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonArcher = false;
                        cooldDown = archerSpawnCooldown;
                        unitCards[0].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummon(cooldDown));
                     }
                     else if (units[index].name == "Swordsmen" && canSummonSwordsmen == true) {
                        Instantiate(units[index], new Vector3(spanws[2].transform.position.x + offset, spanws[2].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonSwordsmen = false;
                        cooldDown = swordsMenSpawnCooldown;
                        unitCards[1].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonSwordsmen(cooldDown));
                     }
                     else if (units[index].name == "Knight" && canSummonKnight == true) {
                        Instantiate(units[index], new Vector3(spanws[2].transform.position.x + offset, spanws[2].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonKnight = false;
                        cooldDown = knightSpawnCooldown;
                        unitCards[2].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonKnight(cooldDown));
                     }
                     break;
               
                 case "spawn-3":
                     enemiesToDamage = Physics2D.OverlapBox(spanws[3].transform.position, new Vector2(width, height), 0, obj);
                     if (enemiesToDamage != null) {
                           return;
                     }
                     if (units[index].name == "Archer" && canSummonArcher == true) {
                        Instantiate(units[index], new Vector3(spanws[3].transform.position.x + offset, spanws[3].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonArcher = false;
                        cooldDown = archerSpawnCooldown;
                        unitCards[0].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummon(cooldDown));
                     }
                     else if (units[index].name == "Swordsmen" && canSummonSwordsmen == true) {
                        Instantiate(units[index], new Vector3(spanws[3].transform.position.x + offset, spanws[3].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonSwordsmen = false;
                        cooldDown = swordsMenSpawnCooldown;
                        unitCards[1].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonSwordsmen(cooldDown));
                     }
                     else if (units[index].name == "Knight" && canSummonKnight == true) {
                        Instantiate(units[index], new Vector3(spanws[3].transform.position.x + offset, spanws[3].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonKnight = false;
                        cooldDown = knightSpawnCooldown;
                        unitCards[2].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonKnight(cooldDown));
                     }
                     break;

                 case "spawn-4":
                     enemiesToDamage = Physics2D.OverlapBox(spanws[4].transform.position, new Vector2(width, height), 0, obj);
                     if (enemiesToDamage != null) {
                           return;
                     }
                     if (units[index].name == "Archer" && canSummonArcher == true) {
                        Instantiate(units[index], new Vector3(spanws[4].transform.position.x + offset, spanws[4].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonArcher = false;
                        cooldDown = archerSpawnCooldown;
                        unitCards[0].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummon(cooldDown));
                     }
                     else if (units[index].name == "Swordsmen" && canSummonSwordsmen == true) {
                        Instantiate(units[index], new Vector3(spanws[4].transform.position.x + offset, spanws[4].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonSwordsmen = false;
                        cooldDown = swordsMenSpawnCooldown;
                        unitCards[1].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonSwordsmen(cooldDown));
                     }
                     else if (units[index].name == "Knight" && canSummonKnight == true) {
                        Instantiate(units[index], new Vector3(spanws[4].transform.position.x + offset, spanws[4].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonKnight = false;
                        cooldDown = knightSpawnCooldown;
                        unitCards[2].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonKnight(cooldDown));
                     }
                     break;

                case "spawn-5":
                     enemiesToDamage = Physics2D.OverlapBox(spanws[5].transform.position, new Vector2(width, height), 0, obj);
                     if (enemiesToDamage != null) {
                           return;
                     }
                     if (units[index].name == "Archer" && canSummonArcher == true) {
                        Instantiate(units[index], new Vector3(spanws[5].transform.position.x + offset, spanws[5].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonArcher = false;
                        cooldDown = archerSpawnCooldown;
                        unitCards[0].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummon(cooldDown));
                     }
                     else if (units[index].name == "Swordsmen" && canSummonSwordsmen == true) {
                        Instantiate(units[index], new Vector3(spanws[5].transform.position.x + offset, spanws[5].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonSwordsmen = false;
                        cooldDown = swordsMenSpawnCooldown;
                        unitCards[1].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonSwordsmen(cooldDown));
                     }
                     else if (units[index].name == "Knight" && canSummonKnight == true) {
                        Instantiate(units[index], new Vector3(spanws[5].transform.position.x + offset, spanws[5].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonKnight = false;
                        cooldDown = knightSpawnCooldown;
                        unitCards[2].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonKnight(cooldDown));
                     }
                     break;

                 case "spawn-6":
                     enemiesToDamage = Physics2D.OverlapBox(spanws[6].transform.position, new Vector2(width, height), 0, obj);
                     if (enemiesToDamage != null) {
                           return;
                     }
                     if (units[index].name == "Archer" && canSummonArcher == true) {
                        Instantiate(units[index], new Vector3(spanws[6].transform.position.x + offset, spanws[6].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonArcher = false;
                        cooldDown = archerSpawnCooldown;
                        unitCards[0].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummon(cooldDown));
                     }
                     else if (units[index].name == "Swordsmen" && canSummonSwordsmen == true) {
                        Instantiate(units[index], new Vector3(spanws[6].transform.position.x + offset, spanws[6].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonSwordsmen = false;
                        cooldDown = swordsMenSpawnCooldown;
                        unitCards[1].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonSwordsmen(cooldDown));
                     }
                     else if (units[index].name == "Knight" && canSummonKnight == true) {
                        Instantiate(units[index], new Vector3(spanws[6].transform.position.x + offset, spanws[6].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonKnight = false;
                        cooldDown = knightSpawnCooldown;
                        unitCards[2].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonKnight(cooldDown));
                     }
                     break;

                 case "spawn-7":
                     enemiesToDamage = Physics2D.OverlapBox(spanws[7].transform.position, new Vector2(width, height), 0, obj);
                     if (enemiesToDamage != null) {
                           return;
                     }
                     if (units[index].name == "Archer" && canSummonArcher == true) {
                        Instantiate(units[index], new Vector3(spanws[7].transform.position.x + offset, spanws[7].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonArcher = false;
                        cooldDown = archerSpawnCooldown;
                        unitCards[0].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummon(cooldDown));
                     }
                     else if (units[index].name == "Swordsmen" && canSummonSwordsmen == true) {
                         Instantiate(units[index], new Vector3(spanws[7].transform.position.x + offset, spanws[7].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonSwordsmen = false;
                        cooldDown = swordsMenSpawnCooldown;
                        unitCards[1].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonSwordsmen(cooldDown));
                     }
                     else if (units[index].name == "Knight" && canSummonKnight == true) {
                         Instantiate(units[index], new Vector3(spanws[7].transform.position.x + offset, spanws[7].transform.position.y, 0), Quaternion.identity);
                        canSummon = false;
                        canSummonKnight = false;
                        cooldDown = knightSpawnCooldown;
                        unitCards[2].GetComponent<Image>().fillAmount = 0;
                        StartCoroutine(resetSummonKnight(cooldDown));
                     }
                     break;

                 default:
                     break;
              }
           }
        }

    }

    IEnumerator resetSummon(float timer) {
       float test = 0;
       while ( unitCards[0].GetComponent<Image>().fillAmount != 1){
            yield return new WaitForSeconds(timer);
            unitCards[0].GetComponent<Image>().fillAmount = test;
            test += 0.01f;
       }
       canSummon = true;
       canSummonArcher = true;
    }

    IEnumerator resetSummonSwordsmen(float timer) {
       float test = 0;
       while ( unitCards[1].GetComponent<Image>().fillAmount != 1){
            yield return new WaitForSeconds(timer);
            unitCards[1].GetComponent<Image>().fillAmount = test;
            test += 0.01f;
       }
       canSummon = true;
       canSummonSwordsmen = true;
    }

    IEnumerator resetSummonKnight(float timer) {
       float test = 0;
       while ( unitCards[2].GetComponent<Image>().fillAmount != 1){
            yield return new WaitForSeconds(timer);
            unitCards[2].GetComponent<Image>().fillAmount = test;
            test += 0.01f;
       }
       canSummon = true;
       canSummonKnight = true;
    }

    IEnumerator SpecialAbiltiy(float cooldowns) {
       for (int i = 0; i < spanws.Length; i++) {
            Instantiate(units[index], new Vector3(spanws[i].transform.position.x + offset, spanws[i].transform.position.y, 0), Quaternion.identity);
       }
       yield return new WaitForSeconds(cooldowns);
       canSpecialSummon = true;
      GameObject.FindGameObjectWithTag("Player").GetComponent<player_1_movement>().Kill_Count = 0;
      text_2.enabled = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spanws[0].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spanws[1].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spanws[2].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spanws[3].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spanws[4].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spanws[5].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spanws[6].gameObject.transform.position, new Vector3(width, height,1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spanws[7].gameObject.transform.position, new Vector3(width, height,1));
        
    }
}
