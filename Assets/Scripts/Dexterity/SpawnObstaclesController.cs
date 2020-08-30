using UnityEngine;

public class SpawnObstaclesController : MonoBehaviour
{
    [Header("Spawn object")]
    public GameObject obstaclePrefab;
    public GameObject bonusPrefab;

    [Header("Spawn timer")]
    public float spawnTimerStop = 3f;

    private float spawnTimer = 0f;
    private float border = 140f;
    private int spawnNumber = 1;
    private float xMin;
    private float xMax;

    private int spawnBonus = 0;
    private int spawnBonusStop = 3;

    void Start()
    {
        RectTransform panelRectTransform = this.GetComponent<RectTransform>();
        if(panelRectTransform) 
        {
            xMin = panelRectTransform.rect.xMin + border;
            xMax = panelRectTransform.rect.xMax - border;
        }
        SpawnObject();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnTimerStop) 
        {
            spawnTimer = 0f;
            if(spawnTimerStop > 0.3f) 
            {
                spawnTimerStop -= 0.1f * spawnTimerStop;
            }
            SpawnObject();
        }
    }

    private void SpawnObject() 
    {
        if(spawnBonus == spawnBonusStop) 
        {
            spawnBonus = 0;
            SpawnBonus();
        } 
        else 
        {
            spawnBonus += 1;
            SpawnObstacle();
        }
    }

    private void SpawnBonus() 
    {
        GameObject newBonus = Instantiate(bonusPrefab);
        newBonus.transform.SetParent(this.transform, false);
        newBonus.transform.localPosition = new Vector3(Random.Range(xMin, xMax), 600, 0);
    }

    private void SpawnObstacle() 
    {
        GameObject newObstacle = Instantiate(obstaclePrefab);
        newObstacle.transform.SetParent(this.transform, false);
        newObstacle.transform.localPosition = new Vector3(Random.Range(xMin, xMax), 600, 0);
    }
}
