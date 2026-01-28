using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public float shieldDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerActions player = other.GetComponent<PlayerActions>();
            if (player != null)
            {
                player.ActivateShield(shieldDuration);
            }

            Destroy(gameObject);
        }
    }
}
