using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3;
    Animator animator;
    private Vector3 movement;
    private CharacterController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
        //playerController = GetComponent<CharacterController>();
        //animator.applyRootMotion = true;
    }

    void Update()
    {
        // movement.Set(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        // playerController.Move(movementSpeed * Time.deltaTime * movement); isso vai fazer ele andar papai
        // if (movement != Vector3.zero) {
        //     transform.forward = movement;
        // }
        
        movement.Set(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        if (movement != Vector3.zero) {
            transform.forward = movement;
        }
        ControllPlayer();
    }

    void ControllPlayer()
    {
        bool isMoving = movement != Vector3.zero;
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
            //animator.SetInteger("Jump", 0);
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
}
