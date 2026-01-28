using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicPlayer : MonoBehaviour
{
    void Awake()
    {
        // Prevent duplicates
        if (FindObjectsOfType<MenuMusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // Keep this music across scenes
        DontDestroyOnLoad(gameObject);

        // Listen for scene change
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If we are now in the GameScene, stop the menu music
        if (scene.name == "ShipSeletScene")  // ? replace with your actual gameplay scene name
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Clean up the event listener to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
