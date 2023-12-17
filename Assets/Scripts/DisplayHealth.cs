using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    void Start()
    {
        // Find the TextMeshPro component on this GameObject
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Find the PlayerHealth script attached to the player GameObject
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if (textMeshPro != null && playerHealth != null)
        {
            textMeshPro.text = "Health: " + playerHealth.currentHealth;
        }
    }
}
