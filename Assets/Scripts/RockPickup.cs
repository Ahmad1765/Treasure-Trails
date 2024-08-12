using UnityEngine;

public class RockPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerShooting>().PickUpRock();
            Destroy(gameObject);
        }
    }
}
