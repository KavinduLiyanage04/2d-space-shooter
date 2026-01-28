using UnityEngine;

public class pickupItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private ShipGameMode gameMode;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerActions player = other.gameObject.GetComponent<PlayerActions>();
            if (player != null && player.GameMode != null)
            {
                player.GameMode.itemsCollected += 1;
                player.GameMode.itemPickupOn();
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                Debug.LogWarning("Player or GameMode is null!");
            }

        }
    }



}
