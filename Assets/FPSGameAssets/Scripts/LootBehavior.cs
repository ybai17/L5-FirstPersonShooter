using UnityEngine;

public class LootBehavior : MonoBehaviour
{
    public int healAmount = 5;

    public AudioClip lootGetSFX;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);

        if (transform.position.y < Random.Range(1.0f, 3.0f))
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            var playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth) {
                if (lootGetSFX)
                    AudioSource.PlayClipAtPoint(lootGetSFX, transform.position);
                    
                playerHealth.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
