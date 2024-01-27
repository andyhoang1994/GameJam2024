using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnActionOne;
    public event EventHandler OnActionTwo;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.ActionOne.performed += ActionOnePerformed;
        playerInputActions.Player.ActionTwo.performed += ActionTwoPerformed;
    }

    private void ActionOnePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnActionOne?.Invoke(this, EventArgs.Empty);
    }
    private void ActionTwoPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnActionTwo?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
