﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 5;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "floor" || col.gameObject.tag == "player") {
            Destroy(gameObject);
        }
    }
}
