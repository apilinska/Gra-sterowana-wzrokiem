using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(gameObject.tag + " on collision with " + col.gameObject.tag);
        if(col.gameObject.tag == "floor") {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(gameObject.tag + " on trigger with " + col.gameObject.tag);
    }
}
