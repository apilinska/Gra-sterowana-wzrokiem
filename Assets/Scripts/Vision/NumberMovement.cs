using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberMovement : MonoBehaviour
{
    public int step = 50;

    private GameObject number;
    private GameObject parent;

    private float timer = 0f;
    private float timerStop = 0.5f;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    void Start()
    {
        number = this.gameObject;
        parent = number.transform.parent.gameObject;

        RectTransform panelRectTransform = parent.GetComponent<RectTransform>();
        if(panelRectTransform) {
            xMin = panelRectTransform.rect.xMin;
            xMax = panelRectTransform.rect.xMax;
            yMin = panelRectTransform.rect.yMin;
            yMax = panelRectTransform.rect.yMax;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timerStop) {
            TransformPosition();
            timer = 0f;
        }
    }

    private void TransformPosition() {
        Vector3 vector = new Vector3();
        do {
            vector = GetRandomVector();
        } while(OutOfBoundaries(vector));
        number.transform.localPosition += vector;
    }

    private bool OutOfBoundaries(Vector3 vector) {
        var position = number.transform.localPosition + vector;
        return ( position.x < xMin || position.x > xMax || 
            position.y < yMin || position.y > yMax );
    }

    private Vector3 GetRandomVector() {
        Vector3 vector = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
        vector.Normalize();
        vector *= step;
        return vector;
    }
}
