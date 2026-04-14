using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f; // Pause game
        menuUI.SetActive(true);
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        menuUI.SetActive(false); 
        Time.timeScale = 1f;     
    }
}
