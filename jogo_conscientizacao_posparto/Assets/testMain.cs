using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMain : MonoBehaviour
{
    public float movementSpeed = 3;
    Animator animator;
    private Vector3 movement;
    private CharacterController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();
        animator.applyRootMotion = false;
    }

    void Update()
    {
        movement.Set(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        playerController.Move(movementSpeed * Time.deltaTime * movement); 
        if (movement != Vector3.zero) {
            transform.forward = movement;
        }
        
        // movement.Set(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        // if (movement != Vector3.zero) {
        //     transform.forward = movement;
        // }
    }
}
