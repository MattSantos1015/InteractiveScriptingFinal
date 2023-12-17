using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplenishArea : MonoBehaviour
{
    public int healthReplenishAmount = 50;
    public int ammoReplenishAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("target"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            Raycast playerRaycast = other.GetComponent<Raycast>();

            if (playerHealth != null)
            {
                // Replenish health
                playerHealth.ReplenishHealth(healthReplenishAmount);
                Debug.Log("Health Replenished");
            }

            if (playerRaycast != null)
            {
                // Replenish ammo
                playerRaycast.ReplenishAmmo(ammoReplenishAmount);
                Debug.Log("Ammo Replenished");
            }

            // Optionally, you can add effects or sound here to indicate replenishment.

            // Disable the replenishment area after use (uncomment the line below if needed)
            // gameObject.SetActive(false);
        }
    }
}
