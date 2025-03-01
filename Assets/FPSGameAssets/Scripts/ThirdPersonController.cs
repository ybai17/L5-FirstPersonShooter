using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpHeight = 0.4f;
    public float gravity = 5f; //9.81f;
    public float airControl = 10;

    public Transform cameraTransform;

    Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;

    int animStateLocal;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        if (!cameraTransform)
            cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        //input vector
        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        //input = new Vector3(moveHorizontal, 0f, moveVertical);

        input.Normalize();

        //Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + 
        float rotationAngle = cameraTransform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

        if (controller.isGrounded) {
            animStateLocal = 0;

            //jump here
            moveDirection = input;

            //is moving
            if (input.magnitude >= 0.1f) {

                if (moveHorizontal < 0) {
                    animStateLocal = 1; //strafe left
                }
                if (moveHorizontal > 0) {
                    animStateLocal = 5; //strafe right
                }

                if (moveVertical > 0) {
                    animStateLocal = 2; //walk forward
                }
                if (moveVertical < 0) {
                    animStateLocal = 4; //walk backwards
                }
            }

            if (Input.GetButton("Jump")) {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                //Debug.Log("moveDirection.y = " + moveDirection.y);
                animStateLocal = 3;
            } else {
                moveDirection.y = 0;
            }
        } else {
            // mid-air
            input.y = moveDirection.y;

            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        animator.SetInteger("animState", animStateLocal);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * movementSpeed * Time.deltaTime);
    }
}
