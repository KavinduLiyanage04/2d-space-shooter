using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;  // destroys bullet after 5 sec if nothing hit

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move bullet upward (local space)
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemyship") || other.CompareTag("EnemyBoss"))
        {
            // Add logic to damage enemy here if needed
            Destroy(gameObject);
        }
    }
}
