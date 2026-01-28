using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipSelectionManager : MonoBehaviour
{
    public GameObject[] shipPrefabs; // Assign all your ship prefabs in the Inspector
    public static GameObject selectedShipPrefab; // This gets accessed from the game scene

    public void SelectShip(int index)
    {
        if (index < shipPrefabs.Length)
        {
            selectedShipPrefab = shipPrefabs[index];
            Debug.Log("Ship selected: " + selectedShipPrefab.name);
            SceneManager.LoadScene("MainGameScene"); // Replace with your actual game scene name
        }
        else
        {
            Debug.LogWarning("Invalid ship index selected.");
        }


    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


}

