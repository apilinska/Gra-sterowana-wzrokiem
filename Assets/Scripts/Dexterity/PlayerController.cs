using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    public float max = 90.0f;
    private Vector3 targetPos;
    private float distance;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        distance = transform.position.z - Camera.main.transform.position.z;
        targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        targetPos = Camera.main.ScreenToWorldPoint(targetPos);
        Vector3 followXonly = new Vector3(targetPos.x, transform.position.y, transform.position.z);
        followXonly.x = Mathf.Clamp(followXonly.x, -max, max);
        transform.position = Vector3.Lerp(transform.position, followXonly, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "obstacle"){
            SceneManager.LoadScene("DexterityResult");
        } else if(col.gameObject.tag == "bonus"){
            DexterityController.CatchBonus();
        }
    }
}
