using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_ai : MonoBehaviour
{
     [SerializeField]private float speed;
    [SerializeField]private float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1.5f, 6f);
        maxDistance = 9.8f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * speed * Time.deltaTime;
        if (this.transform.position.x >= maxDistance) {
            Destroy(this.gameObject);
        }
    }
}
