using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 100;

    public AudioClip projectileSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot()
    {
        if (projectile) {
            GameObject spell = Instantiate(projectile, transform.position, transform.rotation);

            Rigidbody rb = spell.GetComponent<Rigidbody>();

            if (rb) {
                rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
            }

            if (projectileSound) {
                AudioSource.PlayClipAtPoint(projectileSound, GameObject.FindGameObjectWithTag("Player").transform.position);
            }

            spell.transform.SetParent(transform);
        }
        
    }
}
