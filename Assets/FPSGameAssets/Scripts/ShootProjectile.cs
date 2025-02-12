using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectile;
    public float projectileSpeed = 100;
    public float bulletRange = 20;

    public AudioClip projectileSound;

    [Header("Crosshair Settings")]
    public Image crosshairImage;
    public float animationSpeed = 5; //how fast the reticle color changes
    public Color targetColorDementor;
    Color originalCrosshairColor;
    Vector3 originalCrosshairScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalCrosshairColor = crosshairImage.color;
        originalCrosshairScale = crosshairImage.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (!crosshairImage)
            return;

        CrosshairEffect();
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

    // raycasting to detect if we're looking at something as well as what we're looking at
    void CrosshairEffect()
    {
        RaycastHit hit;

        var step = animationSpeed * Time.deltaTime;

        //camera position, forward direction from camera
        //out hit = the variable to store the output of the Raycast() call into
        //Math.Infinity is an option for the maxDistance argument
        if (Physics.Raycast(transform.position, transform.forward, out hit, bulletRange))
        {
            Debug.Log("Hit something: " + hit.collider.name);

            if (hit.collider.CompareTag("Dementor"))
            {
                //animate crosshair
                crosshairImage.color = Color.Lerp(crosshairImage.color, targetColorDementor, step);

                crosshairImage.transform.localScale = Vector3.Lerp(crosshairImage.transform.localScale,
                                                                    originalCrosshairScale / 2,
                                                                    step);

            }
        } else {
            crosshairImage.color = Color.Lerp(crosshairImage.color, originalCrosshairColor, step);

                crosshairImage.transform.localScale = Vector3.Lerp(crosshairImage.transform.localScale, originalCrosshairScale, step);
        }

    }
}
