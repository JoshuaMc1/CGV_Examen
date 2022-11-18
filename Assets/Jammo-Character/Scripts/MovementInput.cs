using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour
{

    public float Velocity;
    [Space]

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;



    // CODIGO VIEJO

    public float HorizontalMove;
    public float VerticalMove;
    public float Gravity = 9.8f;
    public float FallVelocity;
    public float JumpForce;
    public float SlideVelocity;
    public float SlopeForceDown;
    public float PlayerSpeed;

    //public CharacterController Player;
    // public Camera MainCamera;

    private Vector3 CameraForward;
    private Vector3 CameraRight;
    private Vector3 PlayerInput;
    private Vector3 MovePlayer;
    private Vector3 HitNormal;

    public bool IsOnSlope = false;





    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
    private Vector3 moveVector;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();

        //Player = GetComponent<CharacterController>();





    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();

        //isGrounded = controller.isGrounded;
        //if (isGrounded)
        //{
        //    verticalVel -= 0;
        //}
        //else
        //{
        //    verticalVel -= 1;
        //}
        moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
        controller.Move(moveVector);


        // CODIGO VIEJO

        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");

        PlayerInput = new Vector3(HorizontalMove, 0, VerticalMove);
        PlayerInput = Vector3.ClampMagnitude(PlayerInput, 1);

        CameraDirection();

        MovePlayer = (PlayerInput.x * CameraRight) + (PlayerInput.z * CameraForward);
        MovePlayer = MovePlayer * PlayerSpeed;

        //Player.transform.LookAt(Player.transform.position + MovePlayer);

        SetGravity();
        PlayerSkills();

        controller.Move(MovePlayer * Time.deltaTime);
        Debug.Log(controller.velocity.magnitude);


    }

    void CameraDirection()
    {
        CameraForward = cam.transform.forward;
        CameraRight = cam.transform.right;

        CameraForward.y = 0;
        CameraRight.y = 0;

        CameraForward = CameraForward.normalized;
        CameraRight = CameraRight.normalized;
    }



    void SetGravity()
    {
        if (controller.isGrounded)
        {
            FallVelocity = -Gravity * Time.deltaTime;
            MovePlayer.y = FallVelocity;
        }
        else
        {
            FallVelocity -= Gravity * Time.deltaTime;
            MovePlayer.y = FallVelocity;
        }
        anim.SetBool("IsRounded", controller.isGrounded);
        SlideDown();
    }

    void PlayerSkills()
    {
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            FallVelocity = JumpForce;
            MovePlayer.y = FallVelocity;
            //anim.Play("Jump");
        }
    }

    public void SlideDown()
    {
        // IsOnSlope = angulo >= anguloMaximoDelCharacterController;
        IsOnSlope = Vector3.Angle(Vector3.up, HitNormal) >= controller.slopeLimit;

        if (IsOnSlope)
        {
            MovePlayer.x += ((1f - HitNormal.y) * HitNormal.x) * SlideVelocity;
            MovePlayer.z += ((1f - HitNormal.y) * HitNormal.z) * SlideVelocity;

            MovePlayer.y += SlopeForceDown;
        }
    }


    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
        }
    }

    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
        //anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

        //Calculate the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player

        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HitNormal = hit.normal;
    }





}

