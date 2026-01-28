using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBoss : MonoBehaviour
{
    [Header("Boss Settings")]
    public float maxHealth = 500f;
    private float currentHealth;

    private ShipGameMode gameMode;

    [Header("Shooting")]
    public GameObject bossBulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f;
    private float shootTimer;

    [Header("Movement")]
    public float moveSpeed = 2f;
    private Vector2 moveDirection;

    void Start()
    {
        currentHealth = maxHealth;

        gameMode = FindObjectOfType<ShipGameMode>();

        if (gameMode != null && gameMode.bossHealthSlider != null)
        {
            gameMode.bossHealthSlider.maxValue = maxHealth;
            gameMode.bossHealthSlider.value = currentHealth;
            Debug.Log("? BossHealthSlider connected: max = " + maxHealth);
        }
        else
        {
            Debug.LogWarning("? BossHealthSlider is NOT assigned in ShipGameMode!");
        }

        moveDirection = new Vector2(Random.value > 0.5f ? 1f : -1f, 0f);
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.05f || pos.x > 0.95f)
        {
            moveDirection.x = -moveDirection.x;
        }
    }

    void HandleShooting()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            ShootSpread();
            shootTimer = 0f;
        }
    }

    void ShootSpread()
    {
        float bulletSpeed = 3f;

        GameObject center = Instantiate(bossBulletPrefab, firePoint.position, Quaternion.identity);
        center.GetComponent<Rigidbody>().linearVelocity = Vector3.down * bulletSpeed;

        GameObject left = Instantiate(bossBulletPrefab, firePoint.position, Quaternion.identity);
        left.GetComponent<Rigidbody>().linearVelocity = (Vector3.down + Vector3.left * 0.9f).normalized * bulletSpeed;

        GameObject right = Instantiate(bossBulletPrefab, firePoint.position, Quaternion.identity);
        right.GetComponent<Rigidbody>().linearVelocity = (Vector3.down + Vector3.right * 0.9f).normalized * bulletSpeed;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("?? Boss took damage! Current health: " + currentHealth);

        if (gameMode != null && gameMode.bossHealthSlider != null)
        {
            gameMode.bossHealthSlider.value = Mathf.Max(currentHealth, 0);
            Debug.Log("? Updated bossHealthSlider to: " + gameMode.bossHealthSlider.value);
        }
        else
        {
            Debug.LogWarning("? Could not update bossHealthSlider (null reference).");
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("?? Boss defeated!");

        gameMode?.AddScore(1000);
        Destroy(gameObject);
        SceneManager.LoadScene("victory");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("?? Boss hit by bullet!");
            TakeDamage(10f);
            Destroy(other.gameObject);
        }
    }
}
