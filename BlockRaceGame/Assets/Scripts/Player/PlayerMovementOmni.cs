using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovementOmni : NetworkBehaviour
{
    [Header("References: ")]
    [SerializeField] InputReaderOmni inputReader;

    [Header("Settings: ")]
    [SerializeField] float playerSpeed;
    [SerializeField] float playerTurnRate = 180f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float groundCheckDistance = 0.5f;

    private Rigidbody rb;
    private Vector3 previousMoveInput;
    private Vector3 previousRotationInput;

    public override void OnNetworkSpawn()
    {
        if(!IsOwner) return;

        rb = GetComponent<Rigidbody>();

        //Move to OnEnable
        inputReader.OnBoostEvent += HandleBoost;
    }

    private void HandleBoost(bool boostPressed)
    {
        if(boostPressed)
        {

        }
    }

    private void FixedUpdate()
    {
        if(!IsOwner ) return;

        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        Vector2 moveInput = inputReader.GetPlayerMovement();
        Vector3 playerMovement = new Vector3(0 , 0, moveInput.y);
        
        rb.velocity = (transform.forward) * playerSpeed * moveInput.y;        

        if(moveInput.x != 0)
        {
            transform.Rotate(0, moveInput.x * playerTurnRate, 0);
        }
        
    }

    private void HandleJump(bool isJumping)
    {
        if (isJumping && IsGrounded())
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
