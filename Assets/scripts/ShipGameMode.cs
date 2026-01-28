using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class ShipGameMode : MonoBehaviour
{
    public PlayerActions player;
    public TextMeshProUGUI healthdisplay;
    public bool gameOver = false;
    public TextMeshProUGUI gameOverDisplay;
    public Slider healthSlider;
    public TextMeshProUGUI itemsDisplay;
    public int itemsCollected = 0;
    public TextMeshProUGUI itemPickupText;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public Image pilotImage;
    public Sprite alivePilotSprite;
    public Sprite injuredPilotSprite;
    public Sprite deadPilotSprite;

    public Animator pilotAnimator;


    public TextMeshProUGUI bossHealthText;
    public Slider bossHealthSlider;  // ✅ HERE: single boss health slider reference

    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    private float bossTimer = 0f;
    public float bossSpawnDelay = 60f;
    private bool bossSpawned = false;

    public GameObject[] enemyPrefabs;
    public Transform[] enemySpawnPoints;
    public float enemySpawnInterval = 3f;
    private float enemySpawnTimer = 0f;

    public GameObject bulletTemplate;
    public float bulletSpeed = 10f;

    public GameObject winScreen;

    public Transform spawnPoint;



    void Start()
    {
        if (pilotImage != null && alivePilotSprite != null)
        {
            pilotImage.sprite = alivePilotSprite;
        }

        if (ShipSelectionManager.selectedShipPrefab != null)
        {
            GameObject playerObj = Instantiate(ShipSelectionManager.selectedShipPrefab, spawnPoint.position, Quaternion.identity);
            player = playerObj.GetComponent<PlayerActions>();
        }

        // ✅ Safely hide boss health UI after player setup
        if (bossHealthSlider != null)
        {
            bossHealthSlider.gameObject.SetActive(false);
            Debug.Log("✅ Boss Health Slider hidden on Start.");
        }
        else
        {
            Debug.LogWarning("⚠ bossHealthSlider is not assigned!");
        }

        if (bossHealthText != null)
        {
            bossHealthText.gameObject.SetActive(false);
            Debug.Log("✅ Boss Health Text hidden on Start.");
        }
        else
        {
            Debug.LogWarning("⚠ bossHealthText is not assigned!");
        }
    }


    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }


    public void UpdatePilotImage()
    {
        if (player != null && pilotImage != null)
        {
            if (player.health <= 0f && deadPilotSprite != null)
            {
                pilotImage.sprite = deadPilotSprite;
            }
            else if (player.health < 50f && injuredPilotSprite != null)
            {
                pilotImage.sprite = injuredPilotSprite;
            }
            else if (alivePilotSprite != null)
            {
                pilotImage.sprite = alivePilotSprite;
            }
        }
    }

    public void HandlePlayerDeath()
    {
        gameOver = true;

        if (player != null)
        {
            // Disable player controls + visuals
            player.GetComponent<Collider>().enabled = false;
            player.GetComponent<SpriteRenderer>().enabled = false;

            // Trigger pilot death animation if Animator is set
            if (player.pilotAnimator != null)
            {
                player.pilotAnimator.SetTrigger("SetDead");
            }
        }

        if (gameOverDisplay != null)
        {
            gameOverDisplay.gameObject.SetActive(true);
        }
    }


    void Update()
    {
       

        if (!bossSpawned)
        {
            bossTimer += Time.deltaTime;

            enemySpawnTimer += Time.deltaTime;
            if (enemySpawnTimer >= enemySpawnInterval)
            {
                SpawnEnemy();
                enemySpawnTimer = 0f;
            }

            if (bossTimer >= bossSpawnDelay)
            {
                SpawnBoss();
                bossSpawned = true;
            }
        }

        UpdatePilotImage();  // ← call here to keep image updated with health
    
}

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || enemySpawnPoints.Length == 0)
            return;

        int prefabIndex = Random.Range(0, enemyPrefabs.Length);
        int spawnPointIndex = Random.Range(0, enemySpawnPoints.Length);

        Instantiate(enemyPrefabs[prefabIndex], enemySpawnPoints[spawnPointIndex].position, Quaternion.identity);
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);

        bossHealthSlider.gameObject.SetActive(true);
        bossHealthText.gameObject.SetActive(true);

        GameObject bgAudioObj = GameObject.Find("BackgroundAudio");
        if (bgAudioObj != null)
        {
            AudioSource bgAudioSource = bgAudioObj.GetComponent<AudioSource>();
            if (bgAudioSource != null)
            {
                bgAudioSource.Stop();
                Debug.Log("✅ Background music stopped for boss fight!");
            }
        }



        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemyship");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void ShowWinScreen()
    {
        SceneManager.LoadScene("victory"); // replace with exact scene name
    }

        public void itemPickupOn()
    {
        if (itemPickupText != null)
        {
            itemPickupText.text = "";
        }
        else
        {
            Debug.LogWarning("Item Pickup Text is not assigned!");
        }
    }


}


/*
Debug.Log("Ship prefab in GameMode: " + ShipSelectionManager.selectedShipPrefab);
if (ShipSelectionManager.selectedShipPrefab != null)
{
    GameObject playerObj = Instantiate(ShipSelectionManager.selectedShipPrefab, spawnPoint.position, Quaternion.identity);
    player = playerObj.GetComponent<PlayerActions>();

    if (player == null)
    {
        Debug.LogWarning("Spawned ship is missing PlayerActions script.");
    }
}
else
{
    Debug.LogWarning("No ship prefab selected!");
}
*/