using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]

public class Player_Movement : MonoBehaviour
{
    [Header("Player Information")]
    public CharacterController player_CharacterController;
    public Rigidbody player_Rigidbody;
    public Transform player_Transform;

    [Header("Movement Variables")]
    public Vector3 player_Movement;
    public int player_WalkSpeed = 1000;
    public int player_RunSpeed = 2000;
    public int player_JumpSpeed = 7;

    [Header("Interation Variables")]
    public float player_GravityMultiplier = 1.0f;
    public float player_GroundStickForce = 1.0f;

    [Header("Boolean Variables")]
    public bool player_IsIdle;
    public bool player_IsGrounded;
    [Space(5)]
    public bool player_IsMovingForward;
    public bool player_IsMovingBackward;
    public bool player_IsMovingLeft;
    public bool player_IsMovingRight;
    public bool player_IsRunning;
    [Space(5)]
    public bool player_IsJumping;
    public bool player_IsFalling;
    public bool player_IsLanding;

    
    void Start()
    {
        player_Transform = gameObject.transform;
        player_CharacterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        VariableUpdates();
    }


    void VariableUpdates()
    {
        player_IsMovingForward = (Input.GetAxis("Vertical") > 0) ? true : false;
        player_IsMovingBackward = (Input.GetAxis("Vertical") < 0) ? true : false;
        player_IsMovingLeft = (Input.GetAxis("Horizontal") < 0) ? true : false;
        player_IsMovingRight = (Input.GetAxis("Horizontal") > 0) ? true : false;
        player_IsRunning = (Input.GetButton("Run") && !player_IsIdle) ? true : false;

        player_IsGrounded = (player_CharacterController.isGrounded) ? true : false;
        player_IsJumping = (Input.GetButtonDown("Jump") && player_IsGrounded) ? true : false;
        player_IsLanding = (player_IsGrounded && player_IsFalling) ? true : false;
        player_IsFalling = (!player_IsGrounded && !player_IsJumping) ? true : false;

        player_IsIdle = (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && !player_IsRunning && player_IsGrounded) ? true : false;
    }


    void FixedUpdate()
    {
        PlayerMovement();
    }


    void PlayerMovement()
    {
        JumpGravtityStick();

        player_Movement.x = Input.GetAxis("Horizontal") * player_WalkSpeed * Time.fixedDeltaTime;
        player_Movement.z = Input.GetAxis("Vertical") * player_WalkSpeed * Time.fixedDeltaTime;
        
        if (player_IsRunning)
        {
            player_Movement.x = Input.GetAxis("Horizontal") * player_RunSpeed * Time.fixedDeltaTime;
            player_Movement.z = Input.GetAxis("Vertical") * player_RunSpeed * Time.fixedDeltaTime;
        }

        player_CharacterController.Move(player_Transform.localRotation * player_Movement * Time.fixedDeltaTime);
    }


    void JumpGravtityStick()
    {
        if (player_IsGrounded)
        {
            player_Movement.y = -player_GroundStickForce;

            if (player_IsJumping)
            {
                player_Movement.y = player_JumpSpeed;
            }
        }
        else
        {
            player_Movement += Physics.gravity * player_GravityMultiplier * Time.fixedDeltaTime;
        }
    }
}