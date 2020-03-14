using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleController : MonoBehaviour
{
    [Header("Animation clips")]
    public AnimationClip fade_out;

    private Animation anim;
    private float fade_out_duration;

    void Start()
    {
        anim = GetComponent<Animation>();
        anim.AddClip(fade_out, "fade_out");
        fade_out_duration = anim.GetClip("fade_out").length;

        GetComponent<Rigidbody2D>().gravityScale = 5;
    }

    void Update()
    {
        
    }

    IEnumerator playFadeOutAnimation() {
        anim.Play("fade_out");
        yield return new WaitForSeconds(fade_out_duration);
        Destroy(gameObject);
        DexterityController.DestroyObstacle();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "floor" || col.gameObject.tag == "obstacle") {
            gameObject.tag = "obstacleFading";
            StartCoroutine(playFadeOutAnimation());
        }
    }
}
