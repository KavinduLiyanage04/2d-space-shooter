using UnityEngine;

public class itemAction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {

            transform.position = Vector3.Lerp(transform.position, other.gameObject.transform.position, 0.01f);
        


        }
    }
}
