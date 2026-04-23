using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {

        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}