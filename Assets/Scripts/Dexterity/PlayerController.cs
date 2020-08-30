using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : DbConnect
{
    public float speed = 2.0f;
    public float max = 90.0f;
    private Vector3 targetPos;
    private Vector3? cursorPos;
    private float distance;

    void Start()
    {
        targetPos = transform.position;
    }

    private DeviceData GetDeviceData() {
        return GetComponent<DeviceData>();
    }

    private void MoveByMouse() 
    {
        MovePlayer(Input.mousePosition);
    }

    private void MoveByTracker() 
    {
        DeviceData device = GetDeviceData();
        if(device != null) 
        {
            cursorPos = device.CursorPosition();
            if(cursorPos != null) {
                MovePlayer((Vector3)cursorPos);
            }
        }
    }

    private void MovePlayer(Vector3 cursor) 
    {
        distance = transform.position.z - Camera.main.transform.position.z;
        targetPos = new Vector3(cursor.x, cursor.y, distance);
        targetPos = Camera.main.ScreenToWorldPoint(targetPos);
        Vector3 followXonly = new Vector3(targetPos.x, transform.position.y, transform.position.z);
        followXonly.x = Mathf.Clamp(followXonly.x, -max, max);
        transform.position = Vector3.Lerp(transform.position, followXonly, speed * Time.deltaTime);
    }

    void Update()
    {
        if(GameData.IsSimulationMode()) 
        {
            MoveByMouse();
        } 
        else 
        {
            MoveByTracker();
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "obstacle") 
        {
            int score = DexterityController.CalculateResult();
            DexterityInsertScore(score);
            SceneManager.LoadScene("DexterityResult");
        } 
        else if(col.gameObject.tag == "bonus") 
        {
            DexterityController.CatchBonus();
        }
    }
}
