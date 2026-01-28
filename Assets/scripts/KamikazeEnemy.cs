using UnityEngine;
using System.Collections;

public class KamikazeEnemy : MonoBehaviour
{
    public float speed = 2f;
    public RuntimeAnimatorController Explosion;
    private Transform player;
    private ShipGameMode gameMode;
    public GameObject shieldPickupPrefab;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        gameMode = FindObjectOfType<ShipGameMode>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            if (gameMode != null)
            {
                gameMode.AddScore(50);
            }

            // 15% chance to spawn shield pickup
            if (Random.value < 0.5f && shieldPickupPrefab != null)
            {
                Instantiate(shieldPickupPrefab, transform.position, Quaternion.identity);
            }

            StartCoroutine(destroyActor());
        }
        else if (other.CompareTag("Player"))
        {
            PlayerActions playerScript = other.GetComponent<PlayerActions>();
            if (playerScript != null)
            {
                if (playerScript.HasShield)
                {
                    Debug.Log("Player shield absorbed kamikaze hit!");
                    // No damage to player, just destroy enemy
                }
                else
                {
                    playerScript.health -= 50f;  // Damage to player
                    if (gameMode != null && gameMode.healthSlider != null)
                    {
                        gameMode.healthSlider.value = playerScript.health;
                    }

                    if (playerScript.health <= 0)
                    {
                        gameMode?.HandlePlayerDeath();
                    }
                }

                StartCoroutine(destroyActor());
            }
        }
    }

    public IEnumerator destroyActor()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().runtimeAnimatorController = Explosion;

        yield return new WaitForSeconds(2.0f);

        Destroy(this.gameObject);
    }
}
