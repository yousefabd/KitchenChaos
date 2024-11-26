using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform playerHoldPoint;

    public static Player Instance { get; private set; }

    private Vector3 lastInteractionDir;
    private bool isWalking = false;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    public event Action<BaseCounter> OnChangedSelectedCounter;
    public event EventHandler OnPickupObject;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        inputManager.OnInteract += InputManager_OnInteract; ;
        inputManager.OnInteractAlternate += InputManager_OnInteractAlternate;
    }

    private void InputManager_OnInteractAlternate()
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void InputManager_OnInteract()
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        if (GameManager.Instance.IsGamePlaying())
        {
            HandleMovement();
            HandleInteractions();
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVec = inputManager.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);

        float playerDistance = Time.deltaTime * speed;
        float playerHeight = 1.0f;
        float playerRadius = 0.7f;

        isWalking = (moveDir != Vector3.zero);
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, playerDistance);
        if (canMove)
        {
            transform.position += moveDir * playerDistance;
        }
        else if (moveDir.x != 0f && moveDir.z != 0f)
        {
            Vector3 moveDirX = new(moveDir.x, 0f, 0f);
            Vector3 moveDirZ = new(0f, 0f, moveDir.z);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, playerDistance);
            if (canMove)
            {
                transform.position += moveDirX.normalized * playerDistance;
            }
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, playerDistance);
            if (canMove)
            {
                transform.position += moveDirZ.normalized * playerDistance;
            }
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDir, playerDistance);
    }
    private void HandleInteractions()
    {
        Vector2 inputVec = inputManager.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);
        if (moveDir != Vector3.zero) 
        {
            lastInteractionDir = moveDir;
        }
        float interactionDistance = 1.5f;
        if(Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit hitInfo, interactionDistance,layerMask))
        {
            if(hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                SelectCounter(baseCounter);
            }
            else
            {
                SelectCounter(null);
            }
        }
        else
        {
            SelectCounter(null);
        }
    }
    private void SelectCounter(BaseCounter c)
    {
        selectedCounter = c;
        OnChangedSelectedCounter?.Invoke(selectedCounter);
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    public Transform GetObjectSpawnPoint()
    {
        return playerHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        if(kitchenObject != null)
        {
            OnPickupObject?.Invoke(this, EventArgs.Empty);
        }
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
