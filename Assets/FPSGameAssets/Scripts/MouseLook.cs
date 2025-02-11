using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSens = 100;

    Transform playerBody;

    float pitch;

    public float pitchMin = -90;
    public float pitchMax = 90;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerBody = GameObject.FindGameObjectWithTag("Player").transform;
        playerBody = transform.parent.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        // Debug.Log("moveX = " + moveX + "; moveY = " + moveY);

        //yaw, rotate about y axis. Can rotate the entire player
        if (playerBody) {
            playerBody.Rotate(Vector3.up, moveX);
        }
        
        //pitch, rotate about x axis.
        // Can only rotate the camera (otherwise, you would walk through the ground if you looked down)
        pitch -= moveY;

        //need to ensure player can only look up and down without breaking their neck
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        transform.localRotation = Quaternion.Euler(pitch, 0, 0);

    }
}
