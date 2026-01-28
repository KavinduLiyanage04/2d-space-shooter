using UnityEngine;
using System.Collections;


public class PlayerActions : MonoBehaviour
{
    public GameObject bulletTemplate;   // Bullet prefab (assign in Inspector)
    public float bulletSpeed = 10f;     // Speed to assign to bullet
    public float health = 100f;         // Player health
    public Transform firePoint;
    public AudioClip shootClip;

    private AudioSource audioSource;


    public Animator pilotAnimator;
    private ShipGameMode gameMode;
    public bool isShielded = false;
    public bool HasShield { get; private set; } = false;

    public ShipGameMode GameMode => gameMode;  // Public getter
    public GameObject shieldVisual; // assign in Inspector


    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        gameMode = FindObjectOfType<ShipGameMode>();

        if (gameMode != null)
        {
            gameMode.player = this;

            // Optional: center player or set starting position
            transform.position = new Vector3(0f, 2f, 0f);  // adjust if needed
        }
        else
        {
            Debug.LogWarning("⚠ PlayerActions could not find ShipGameMode in scene!");
        }
        /*
        gameMode = FindFirstObjectByType<ShipGameMode>();

        if (gameMode != null)
        {
            gameMode.player = this;
        }
        else
        {
            Debug.LogWarning("⚠ PlayerActions could not find ShipGameMode in scene!");
        }
        */
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float moveSpeed = 5f;  // or whatever feels right

        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-1f, 0f, 0f) * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(1f, 0f, 0f) * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0f, 1f, 0f) * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0f, -1f, 0f) * moveSpeed * Time.deltaTime;
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameMode.gameOver)
            {
                GameObject bullet = Instantiate(bulletTemplate, firePoint.position, Quaternion.identity);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = firePoint.up * bulletSpeed;
                    Debug.Log("Bullet velocity set to: " + rb.velocity);
                }
                else
                {
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    if (bulletScript != null)
                    {
                        bulletScript.speed = bulletSpeed;
                    }
                    if (shootClip != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(shootClip);
                    }
                }
            }
        }
    }

    public void ActivateShield()
    {
        isShielded = true;
        Debug.Log("🛡 Damage Barrier Active!");

        if (gameMode != null && gameMode.itemPickupText != null)
        {
            gameMode.itemPickupText.text = "Damage Barrier Active!";
            gameMode.itemPickupText.gameObject.SetActive(true);
        }

        StartCoroutine(ShieldDuration());
    }

    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(5f);
        isShielded = false;

        if (gameMode != null && gameMode.itemPickupText != null)
        {
            gameMode.itemPickupText.gameObject.SetActive(false);
        }

        Debug.Log("🛡 Damage Barrier Ended!");
    }

    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldCoroutine(duration));
    }

    private IEnumerator ShieldCoroutine(float duration)
    {
        HasShield = true;
        if (shieldVisual != null)
            shieldVisual.SetActive(true);

        Debug.Log("🛡 Shield activated!");

        yield return new WaitForSeconds(duration);

        HasShield = false;
        if (shieldVisual != null)
            shieldVisual.SetActive(false);

        Debug.Log("🛡 Shield deactivated.");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShieldPickup"))
        {
            Debug.Log("🛡 Shield pickup collected!");
            ActivateShield(5f); // 5-second shield
            Destroy(other.gameObject);

            if (gameMode != null)
            {
                gameMode.itemPickupText.text = "SHIELD";
                gameMode.itemPickupText.gameObject.SetActive(true);
                StartCoroutine(HideShieldText());
            }
        }
        else if (other.CompareTag("enemybullet"))
        {
            if (!HasShield)
            {
                health -= 10f;
                if (gameMode != null && gameMode.healthSlider != null)
                    gameMode.healthSlider.value = health;

                if (gameMode != null)
                {
                    gameMode.UpdatePilotImage();
                }


                if (health <= 0)
                    gameMode?.HandlePlayerDeath();
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemyship") || other.CompareTag("kamikaze"))
        {
            if (!HasShield)
            {
                health -= 50f;
                if (gameMode != null && gameMode.healthSlider != null)
                    gameMode.healthSlider.value = health;

                if (health <= 0)
                    gameMode?.HandlePlayerDeath();
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator HideShieldText()
    {
        yield return new WaitForSeconds(2f);
        if (gameMode != null && gameMode.itemPickupText != null)
            gameMode.itemPickupText.gameObject.SetActive(false);
    }

}
