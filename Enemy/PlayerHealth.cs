using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Update the health UI at the start (if any)
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0

        Debug.Log("Player takes " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            UpdateHealthUI(); // Update the health UI after taking damage
        }
    }

    // Method to heal
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't exceed maxHealth

        Debug.Log("Player heals " + amount + ". Current health: " + currentHealth);
        UpdateHealthUI(); // Update the health UI after healing
    }

    // Method to handle player death
    void Die()
    {
        Debug.Log("Player has died!");
        // Implement death logic here (e.g., respawn, game over screen, etc.)
        // Destroy(gameObject); // Uncomment if you want to destroy the player object on death
    }

    // Method to update the health UI (if any)
    void UpdateHealthUI()
    {
        // Implement health UI update logic here (e.g., updating health bar, etc.)
    }
}
