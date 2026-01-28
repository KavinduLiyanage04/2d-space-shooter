using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerActions player = other.GetComponent<PlayerActions>();
            if (player != null)
            {
                player.health -= 10; // Adjust damage as needed
            }
            Destroy(gameObject);
        }

        // Optional: Destroy if hits anything else or goes off screen
        if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
