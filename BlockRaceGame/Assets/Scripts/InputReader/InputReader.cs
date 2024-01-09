using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<bool> JumpEvent;
    public event Action<Vector3> MoveEvent;

    private Controls controls;

    private void OnEnable()
    {
        if(UnityEngine.InputSystem.Gyroscope.current != null)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
        if(AttitudeSensor.current != null)
        {
            InputSystem.EnableDevice(AttitudeSensor.current);
        }
        

        if(controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }

        controls.Player.Enable();
    }

    private void OnDisable()
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            JumpEvent?.Invoke(true);
        }
        else if(context.canceled)
        {
            JumpEvent?.Invoke(false);
        }
        
    }

    //Steering with the gyroscope / accelerometer
    public void OnSteering(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Moving");
        }
        MoveEvent?.Invoke(context.ReadValue<Vector3>());    
    }

    public void OnSteeringKeyboard(InputAction.CallbackContext context)
    {
        //MoveEvent?.Invoke(context.ReadValue<Vector2>());
        Vector2 input = context.ReadValue<Vector2>();
        Vector3 moveInput = new Vector3(input.x, 0, 0);
        MoveEvent?.Invoke(moveInput);
    }
}
