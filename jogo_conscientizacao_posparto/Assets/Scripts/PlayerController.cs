using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -20f;
    public float jumpHeight = 1.5f;
    public float rotationSpeed = 10f;
    public float airControl = 0.6f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 inputDirection;
    private Animator animator;

    private bool isJumping = false;
    private float initialJumpY;
    private float jumpTimeCounter;
    private float jumpDuration = 0.4f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
    }

    void Update()
    {
        HandleMovement();

        bool isMoving = inputDirection != Vector3.zero;

        ControllPlayer(isMoving);
    }

    void ControllPlayer(bool isMoving)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (isMoving && stateInfo.IsName("MainCharacterIdle"))
        {
            animator.SetInteger("PreJump", 1);
        }
        else if (!isMoving && stateInfo.IsName("MainCharacterPreJump"))
        {
            animator.SetInteger("PreJump", 0);
        }

        if (stateInfo.IsName("MainCharacterPreJump") && isMoving)
        {
            animator.SetInteger("Jump", 1);
        }

        if (stateInfo.IsName("MainCharacterJump"))
        {
            animator.SetInteger("Landing", 1);
        }

        if (stateInfo.IsName("MainCharacterLanding"))
        {
            if (!isMoving)
            {
                animator.SetInteger("PreJump", 0);
                animator.SetInteger("Jump", 0);
                animator.SetInteger("Landing", 0);
            }
            else
            {
                animator.SetInteger("PreJump", 2);
            }
        }
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(x, 0, z).normalized;

        if (inputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (controller.isGrounded && !stateInfo.IsName("MainCharacterJump"))
        {
            velocity.y = -2f;
            isJumping = false;
        }

        if (stateInfo.IsName("MainCharacterJump"))
        {
            if (!isJumping)
            {
                isJumping = true;
                jumpTimeCounter = 0f;
                initialJumpY = transform.position.y;
            }

            if (jumpTimeCounter < jumpDuration)
            {
                float jumpProgress = jumpTimeCounter / jumpDuration;
                float jumpVelocity = Mathf.Lerp(jumpHeight, 0, jumpProgress);
                velocity.y = jumpVelocity;
                jumpTimeCounter += Time.deltaTime;
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }

            Vector3 move = inputDirection * moveSpeed * airControl;
            move.y = velocity.y;
            controller.Move(move * Time.deltaTime);
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;

            Vector3 move = inputDirection * moveSpeed;
            move.y = velocity.y;
            controller.Move(move * Time.deltaTime);
        }
    }
}
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     public float gravity = -20f;
//     public float jumpHeight = 1.5f;
//     public float rotationSpeed = 10f;
//     public float airControl = 0.6f;

//     private CharacterController controller;
//     private Vector3 velocity;
//     private Vector3 inputDirection;
//     private Animator animator;

//     private bool isJumping = false;
//     private float initialJumpY;
//     private float jumpTimeCounter;
//     private float jumpDuration = 0.4f;

//     void Start()
//     {
//         controller = GetComponent<CharacterController>();
//         animator = GetComponent<Animator>();
//         animator.applyRootMotion = true;
//     }

//     void Update()
//     {
//         HandleMovement();

//         bool isMoving = inputDirection != Vector3.zero;

//         ControllPlayer(isMoving);
//     }

//     void ControllPlayer(bool isMoving)
//     {
//         AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

//         if (isMoving && stateInfo.IsName("MainCharacterIdle"))
//         {
//             animator.SetInteger("PreJump", 1);
//         }
//         else if (!isMoving && stateInfo.IsName("MainCharacterPreJump"))
//         {
//             animator.SetInteger("PreJump", 0);
//         }

//         if (stateInfo.IsName("MainCharacterPreJump") && isMoving)
//         {
//             animator.SetInteger("Jump", 1);
//         }

//         if (stateInfo.IsName("MainCharacterJump"))
//         {
//             animator.SetInteger("Landing", 1);
//         }

//         if (stateInfo.IsName("MainCharacterLanding"))
//         {
//             if (!isMoving)
//             {
//                 animator.SetInteger("PreJump", 0);
//                 animator.SetInteger("Jump", 0);
//                 animator.SetInteger("Landing", 0);
//             }
//             else
//             {
//                 animator.SetInteger("PreJump", 2);
//             }
//         }
//     }

//     void HandleMovement()
// {
//     float x = Input.GetAxisRaw("Horizontal");
//     float z = Input.GetAxisRaw("Vertical");

//     inputDirection = new Vector3(x, 0, z).normalized;

//     if (inputDirection != Vector3.zero)
//     {
//         Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
//         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
//     }

//     AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

//     if (controller.isGrounded && !stateInfo.IsName("MainCharacterJump"))
//     {
//         velocity.y = -2f;
//         isJumping = false;
//     }

//     if (!controller.isGrounded)
//     {
//         if (stateInfo.IsName("MainCharacterJump"))
//         {
//             if (!isJumping)
//             {
//                 isJumping = true;
//                 jumpTimeCounter = 0f;
//                 initialJumpY = transform.position.y;
//             }

//             if (jumpTimeCounter < jumpDuration)
//             {
//                 float jumpProgress = jumpTimeCounter / jumpDuration;
//                 float jumpVelocity = Mathf.Lerp(jumpHeight, 0, jumpProgress);
//                 velocity.y = jumpVelocity;
//                 jumpTimeCounter += Time.deltaTime;
//             }
//             else
//             {
//                 velocity.y += gravity * Time.deltaTime;
//             }
//         }
//         else
//         {
//             velocity.y += gravity * Time.deltaTime;
//         }

//         Vector3 move = inputDirection * moveSpeed * airControl;
//         move.y = velocity.y;
//         controller.Move(move * Time.deltaTime);
//     }
//     else 
//     {
//         velocity.y += gravity * Time.deltaTime;

//         Vector3 move = new Vector3(0, velocity.y, 0);
//         controller.Move(move * Time.deltaTime);
//     }
// }

// }
