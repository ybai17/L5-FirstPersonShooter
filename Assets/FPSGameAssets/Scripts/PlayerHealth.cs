using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    private int currentHealth;
    public AudioClip deathSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

        Debug.Log("OW..." + currentHealth + " HP left");

        if (currentHealth <= 0)
        {
            //player dies
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player DIES");
    }
}
