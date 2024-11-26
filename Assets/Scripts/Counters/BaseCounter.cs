using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private Transform counterTop;
    private KitchenObject kitchenObject;

    public static event EventHandler OnPutDownObject;
    public virtual void Interact(Player player) { }
    public virtual void InteractAlternate(Player player) { }

    public static void ResetStaticFields()
    {
        OnPutDownObject = null;
    }
    public Transform GetObjectSpawnPoint()
    {
        return counterTop;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        if (kitchenObject != null)
        {
            OnPutDownObject?.Invoke(this,EventArgs.Empty);
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
