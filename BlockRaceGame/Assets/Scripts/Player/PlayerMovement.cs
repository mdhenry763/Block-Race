using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References: ")]
    [SerializeField] InputReader inputReader;
    [SerializeField] Transform playerBody;
    [SerializeField] Rigidbody rb;

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

        //Movement
        float xMovement = previousMovement.x * horizontalSpeed;
        rb.velocity = Vector3.right * xMovement;
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
            
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance);
    }


}
