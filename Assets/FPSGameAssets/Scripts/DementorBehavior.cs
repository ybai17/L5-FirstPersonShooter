using UnityEngine;

public class DementorBehavior : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 1;
    public bool randomSpeed = false;
    public float minimumDistance = 1; //will follow the player until they're 1 meter away
    public GameObject particleEffect;
    public int damageValue = 10;

    public GameObject lootPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (randomSpeed)
        {
            moveSpeed = Random.Range(2.0f, 5.0f);
        }

        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        float distanceFromPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceFromPlayer > minimumDistance)
        {
            if (PlayerHealth.IsAlive)
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            } else 
            {
                Invoke("DestroyDementor", 2);
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") || other.CompareTag("Shield")) {
            DestroyDementor();
        } else if (other.CompareTag("Player")) {
            var playerHealth = other.transform.GetComponent<PlayerHealth>();

            if (playerHealth) {
                playerHealth.TakeDamage(damageValue);
            }
        }
    }

    void DestroyDementor()
    {
        if (particleEffect)
        {
            Instantiate(particleEffect, transform.position, transform.rotation);
        }

        if (lootPrefab)
        {
            Instantiate(lootPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
