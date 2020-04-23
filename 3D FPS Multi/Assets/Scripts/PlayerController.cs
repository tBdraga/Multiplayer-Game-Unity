using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Text;

public class PlayerController : MonoBehaviour
{
    GameObject scoreBoard;
    int playerCount;


    float speed = 4;
    float rotationSpeed = 80;
    float rotation = 0;
    float gravity = 20;
    float jumpHeight = 8;

    Vector3 moveDirection = Vector3.zero;

    CharacterController characterController;
    Animator animator;
    Transform cameraTransform;
    //Rigidbody rigidBody;

    void Start()
    {
        //find scoreboard canvas
        scoreBoard = GameObject.Find("Canvas").transform.Find("ScoreBoard").gameObject;

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //rigidBody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        //face the direction the camera faces
        setupFacingDirection();

        Movement();
        GetInput();

        //Scoreboard
        if (Input.GetKeyDown(KeyCode.Tab)) {
            //show scoreboard
            scoreBoard.SetActive(true);
            //update scoreboard
            updateScoreboard();

        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            //hide scoreboard
            scoreBoard.SetActive(false);
        }
    }

    void updateScoreboard() {
        //initialize player count
        playerCount = PhotonNetwork.PlayerList.Length;

        //get player names
        var playerNames = new StringBuilder();

        foreach (var player in PhotonNetwork.PlayerList) {
            //append names
            playerNames.Append(player.NickName);
            playerNames.Append("\n");
        }

        string output = "Online players: " + playerCount.ToString() + "\n" + playerNames.ToString();
        scoreBoard.transform.Find("Text").GetComponent<Text>().text = output;
    }

    void Movement()
    {
        moveForward();
        moveLeft();
        moveLeft45();
        moveRight();
        moveRight45();
        moveBackwards();

        jump();



        //lower player to the ground
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        if (characterController.isGrounded && animator.GetBool("running") == false)
        {
            animator.SetInteger("condition", 0);
        }
    }

    void moveForward() {
        //move forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("running", true);
            animator.SetInteger("condition", 1);
            moveDirection = new Vector3(0, 0, 1);
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("running", false);
            animator.SetInteger("condition", 0);
            moveDirection = new Vector3(0, 0, 0);
        }
    }

    void moveBackwards(){
        //move forward
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("running", true);
            animator.SetInteger("condition", 8);
            moveDirection = new Vector3(0, 0, -1);
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("running", false);
            animator.SetInteger("condition", 0);
            moveDirection = new Vector3(0, 0, 0);
        }
    }

    void moveLeft() {
        //move left
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("running", true);
            animator.SetInteger("condition", 4);
            moveDirection = new Vector3(-1, 0, 0);
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("running", false);
            animator.SetInteger("condition", 0);
            moveDirection = new Vector3(0, 0, 0);
        }
    }

    void moveLeft45() {
        //move left 45
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("running", true);
            animator.SetInteger("condition", 6);
            moveDirection = new Vector3(-1, 0, 1);
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
    }

    void moveRight() {
        //move right
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("running", true);
            animator.SetInteger("condition", 5);
            moveDirection = new Vector3(1, 0, 0);
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("running", false);
            animator.SetInteger("condition", 0);
            moveDirection = new Vector3(0, 0, 0);
        }
    }

    void moveRight45() {
        //move right 45
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("running", true);
            animator.SetInteger("condition", 7);
            moveDirection = new Vector3(1, 0, 1);
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0)) {

            if (animator.GetBool("running") == true)
            {
                animator.SetBool("running", false);
                animator.SetInteger("condition", 0);
            }
            else if (animator.GetBool("running") == false) {
                Attacking();
            }
        }
    }

    void jump() {
        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetInteger("condition", 3);
            moveDirection.y = jumpHeight;
        }
    }

    void Attacking()
    {
        animator.SetInteger("condition", 2);
    }

    void setupFacingDirection()
    {
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }
}
