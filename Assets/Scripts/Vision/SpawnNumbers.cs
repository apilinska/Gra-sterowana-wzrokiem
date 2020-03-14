using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNumbers : MonoBehaviour
{
    public GameObject numberPrefab;
    public int count = 10;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    void Start()
    {
        RectTransform panelRectTransform = this.GetComponent<RectTransform>();
        if(panelRectTransform) {
            xMin = panelRectTransform.rect.xMin;
            xMax = panelRectTransform.rect.xMax;
            yMin = panelRectTransform.rect.yMin;
            yMax = panelRectTransform.rect.yMax;
        }

        SpawnObjects();
    }

    private void SpawnObjects() {
        for(int i=0; i < count; i++) {
            GameObject newNumber = Instantiate(numberPrefab);
            newNumber.GetComponentInChildren<Text>().text = (i+1).ToString();
            newNumber.transform.SetParent(this.transform, false);
            newNumber.transform.localPosition = new Vector3(
                Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        }
    }
}
