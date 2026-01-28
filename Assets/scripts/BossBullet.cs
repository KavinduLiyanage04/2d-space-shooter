using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Always move straight down in world space
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
