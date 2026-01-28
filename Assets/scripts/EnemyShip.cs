using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour
{
    public RuntimeAnimatorController Explosion;

    private Vector3 floatDirection;
    private float floatSpeed = 0.5f;
    private float floatChangeTimer = 0f;
    private float floatChangeInterval = 2f;
    public GameObject enemyBulletPrefab;
    public float shootInterval = 2f;
    private float shootTimer = 0f;
    public Transform gunPoint;
    public ShipGameMode gameMode;
    public GameObject shieldPickupPrefab;
    void Start()
    {
        ChooseNewDirection();
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }




        floatChangeTimer += Time.deltaTime;

        if (floatChangeTimer >= floatChangeInterval)
        {
            ChooseNewDirection();
            floatChangeTimer = 0f;
        }

        transform.position += floatDirection * floatSpeed * Time.deltaTime;

        // Optionally destroy if way off-screen
        if (transform.position.y < -8.0f || Mathf.Abs(transform.position.x) > 10.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void ChooseNewDirection()
    {
        float dirX = Random.Range(-1f, 1f);
        float dirY = Random.Range(-0.2f, 0.2f);
        floatDirection = new Vector3(dirX, dirY, 0f).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            StartCoroutine(destroyActor(other.gameObject));
        }
    }


    public IEnumerator destroyActor(GameObject bullet)
    {
        Destroy(bullet);
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().runtimeAnimatorController = Explosion;

        ShipGameMode gameMode = FindFirstObjectByType<ShipGameMode>();
        if (gameMode != null)
        {
            gameMode.AddScore(100); // Add 100 points per enemy
        }

        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);

        if (bullet != null)
            Destroy(bullet);

        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().SetTrigger("Explode");

        yield return new WaitForSeconds(2f);

        // 15% chance to spawn shield pickup
        if (Random.value < 0.5f)
        {
            Instantiate(shieldPickupPrefab, transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }

    void Die()
    {
        if (gameMode != null)
        {
            gameMode.AddScore(100);  // +100 per enemy kill
        }

        // Play explosion, effects, etc.
        Destroy(this.gameObject);
    }


    void Shoot()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, gunPoint.position, gunPoint.rotation);
    }



}
