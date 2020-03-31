using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 4;
    float rotationSpeed = 80;
    float rotation = 0;
    float gravity = 8;

    Vector3 moveDirection = Vector3.zero;

    CharacterController characterController;
    Animator animator;
    public Transform cameraTransform;

    void Start()
    {

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //face the direction the camera faces
        setupFacingDirection();

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetInteger("condition", 1);
            moveDirection = new Vector3(0, 0, 1);
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetInteger("condition", 0);
            moveDirection = new Vector3(0, 0, 0);
        }

        //lower player to the ground
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void setupFacingDirection()
    {
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }
}
