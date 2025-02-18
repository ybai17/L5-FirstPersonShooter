using UnityEngine;

public class Breakable : MonoBehaviour
{
   public GameObject cratePieces;

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(cratePieces, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
