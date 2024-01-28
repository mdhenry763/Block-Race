using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Input Reader Omni", menuName = "Input/Input Reader Omni")]
public class InputReaderOmni : ScriptableObject
{
    public event Action<Vector2> OnMoveEvent;

    private DefaultActions playerActions; 

    private void OnEnable()
    {
        if(playerActions == null)
        {
            playerActions = new DefaultActions();
        }

        playerActions.Enable();

        playerActions.Player.Move.performed += OnMove;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
