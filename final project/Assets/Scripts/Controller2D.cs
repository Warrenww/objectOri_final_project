using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{

    CharacterController characterController;

    public float gravity = 10;
    public float walkSpeed = 5;
    public float jumpHeight = 5;

    Vector3 moveDirection = Vector3.zero;
    float horizontal = 0;
    public float smoothRate = 0.5f;
    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        characterController.Move(moveDirection * Time.deltaTime);
        horizontal = Input.GetAxis("Horizontal");
        moveDirection.y -= gravity * Time.deltaTime;

        if (horizontal > 0.01f)
        {
            moveDirection.x = horizontal * walkSpeed;
        }
        if (horizontal < 0.01f)
        {
            moveDirection.x = horizontal * walkSpeed;
        }
        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpHeight;
            }

        }
    }
}
