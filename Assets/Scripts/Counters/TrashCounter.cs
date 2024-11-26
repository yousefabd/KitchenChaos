using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrash;

    new public static void ResetStaticFields()
    {
        OnTrash = null;
    }
    public override void Interact(Player player)
    {
        if(player.GetKitchenObject() != null)
        {
            OnTrash?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();
        }
    }
}
