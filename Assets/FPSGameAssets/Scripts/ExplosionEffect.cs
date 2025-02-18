using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float explosionForce = 100f;

    public float explosionRadius = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Explode();
    }

    // Update is called once per frame
    void Explode()
    {
        Rigidbody[] pieces = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in pieces) {
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            Debug.Log("BOOM: " + rb.name);
        }

        // Debug.Log("RigidBodies: " + pieces.Length);
    }
}
