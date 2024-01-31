using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Input Reader Omni", menuName = "Input/Input Reader Omni")]
public class InputReaderOmni : ScriptableObject
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnJumpEvent;
    public event Action<bool> OnBoostEvent;

    private DefaultActions playerActions; 

    private void OnEnable()
    {
        if(playerActions == null)
        {
            playerActions = new DefaultActions();
        }

        playerActions.Player.Jump.performed += HandleJump;
        playerActions.Player.Boost.performed += HandleBoost;
        playerActions.Player.Boost.canceled += HandleBoost;

        playerActions.Enable();
    }

    private void HandleBoost(InputAction.CallbackContext obj)
    {
        OnBoostEvent?.Invoke(obj.ReadValue<bool>());
    }

    private void HandleJump(InputAction.CallbackContext obj)
    {
        OnJumpEvent?.Invoke();
    }

    public Vector2 GetPlayerMovement()
    {
        Vector2 inputVector2 = playerActions.Player.Move.ReadValue<Vector2>();
        return inputVector2;
    }
}
