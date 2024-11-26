using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event Action OnGrabObject;
    public override void Interact(Player player)
    {
        if (player.GetKitchenObject() == null)
        {
            KitchenObject.Instantiate(kitchenObjectSO, player);
            OnGrabObject?.Invoke();
        }
    }
}
