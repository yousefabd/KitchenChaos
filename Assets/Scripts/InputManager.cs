using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    public event Action OnInteract;
    public event Action OnInteractAlternate;
    public event Action OnPause;

    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Idle.Enable();
        playerInputActions.Idle.Interact.performed += Interact_performed;
        playerInputActions.Idle.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Idle.Pause.performed += Pause_performed;
    }
    private void OnDestroy()
    {
        playerInputActions.Idle.Interact.performed -= Interact_performed;
        playerInputActions.Idle.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Idle.Pause.performed -= Pause_performed;
        playerInputActions.Dispose();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPause?.Invoke();
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Idle.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
