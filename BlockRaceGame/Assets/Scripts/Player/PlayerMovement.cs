using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References: ")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    [Header("Settings: ")]
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundCheckDistance = 0.5f;


    //Local
    private Vector3 previousMovement;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        inputReader.JumpEvent += HandleJump;
        inputReader.MoveEvent += HandleMovement;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        inputReader.JumpEvent -= HandleJump;
        inputReader.MoveEvent -= HandleMovement;
    }

    private void FixedUpdate()
    {
        if(!IsOwner) return;

        Vector3 movement = Vector3.forward * forwardSpeed + Vector3.right * (previousMovement.x * horizontalSpeed);
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

    }

    private void HandleMovement(Vector3 movementInput)
    {
        previousMovement = movementInput;
    }

    private void HandleJump(bool isJumping)
    {
        if(isJumping && IsGrounded())
        {
            //Perform only when player is touching the ground
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //animator.SetTrigger("OnJump");
            
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance);
    }


}
