using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSPlayerController : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpHeight = 0.4f;
    public float gravity = 5f; //9.81f;
    public float airControl = 10;

    Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        //input vector
        input = transform.right * moveHorizontal + transform.forward * moveVertical;

        input.Normalize();

        if (controller.isGrounded) {
            //jump here
            moveDirection = input;

            if (Input.GetButton("Jump")) {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                //Debug.Log("moveDirection.y = " + moveDirection.y);
            } else {
                moveDirection.y = 0;
            }
        } else {
            // mid-air
            input.y = moveDirection.y;

            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * movementSpeed * Time.deltaTime);
    }
}
