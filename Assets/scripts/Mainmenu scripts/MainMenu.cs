using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ShipSelectScene"); // Replace with your game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!"); // Shows in editor only
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Use the exact name of your main menu scene here
    }
}

