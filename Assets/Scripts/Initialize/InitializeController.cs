using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitializeController : MonoBehaviour
{
    public Button simulation;
    public Button device;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        simulation.onClick.AddListener(() => SetMode(GameMode.Simulation));
        device.onClick.AddListener(() => SetMode(GameMode.Device));
    }

    private void SetMode(GameMode mode) {
        GameData.SetMode(mode);
        SceneManager.LoadScene("Login");
    }
}
