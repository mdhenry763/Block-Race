using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private void Awake()
    {

    }

    void Start()
    {
        
    }

    private void HandleRotation(Vector3 obj)
    {
        Debug.Log(obj.ToString());
    }

    private void HandleJump(bool obj)
    {
        Debug.Log("Jump" + obj);
    }

    private void HandleMovement(Vector3 obj)
    {
        Debug.Log("Movement" + obj.ToString());
    }
}
