using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_ai_bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime;
        if (this.transform.position.x >= maxDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
