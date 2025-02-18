using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public int startingHealth = 100;
    private int currentHealth;

    public static bool IsAlive {get; private set;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = startingHealth;
        IsAlive = true;

        UpdateHealthSlider();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

        UpdateHealthSlider();

        Debug.Log("OW..." + currentHealth + " HP left");

        if (currentHealth <= 0 && IsAlive)
        {
            //player dies
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);

        UpdateHealthSlider();

        Debug.Log("yum, healed: " + currentHealth + " HP left");
    }

    void Die()
    {
        IsAlive = false;

        Debug.Log("Player DIES");

        var audioSource = GetComponent<AudioSource>();

        if (audioSource)
        {
            audioSource.Play();
        }

        transform.Rotate(0, 0, -90, Space.Self);
    }

    void UpdateHealthSlider()
    {
        if (healthSlider)
        {
            healthSlider.value = currentHealth;
        }
    }
}
