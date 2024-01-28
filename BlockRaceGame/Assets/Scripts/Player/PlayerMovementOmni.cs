using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOmni : MonoBehaviour
{
    [Header("References: ")]
    [SerializeField] InputReaderOmni inputReader;

    [Header("Settings: ")]
    [SerializeField] float playerSpeed;
    [SerializeField] float playerTurnRate = 180f;

    private Rigidbody rb;
    private Vector3 previousMoveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Move to OnEnable
        inputReader.OnMoveEvent += HandleMovement;
    }

    private void HandleMovement(Vector2 moveInput)
    {
        previousMoveInput = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        rb.velocity = previousMoveInput * playerSpeed;

        if(previousMoveInput.sqrMagnitude > 0.1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, previousMoveInput, Time.deltaTime * playerTurnRate);
        }
    }
}
