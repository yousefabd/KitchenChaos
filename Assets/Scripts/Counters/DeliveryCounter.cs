using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    OrderSO orderSO;
    private float maxTime = 6f;
    private float currentTime=0f;

    public static event EventHandler OnDeliverySuccess;
    public static event EventHandler OnDeliveryFailed;
    public int successfulRecipesDelivered;

    public static DeliveryCounter Instance { get; private set; }
    
    new public static void ResetStaticFields()
    {
        OnDeliveryFailed = null;
        OnDeliverySuccess = null;
    }
    private void Awake()
    {
        Instance = this;
        orderSO = ScriptableObject.CreateInstance<OrderSO>();
        Orders.Instance.ClearOrders();
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= maxTime)
        {
            Orders.Instance.AddNewOrder();
            currentTime = 0f;
        }
    }
    public override void Interact(Player player)
    {
        if (player.GetKitchenObject() != null) 
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
            {
                orderSO.SetRecipeOrders(plate.GetRecipe());
                bool succ=Orders.Instance.SubmitOrder(orderSO);
                if (succ)
                {
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    successfulRecipesDelivered++;
                }
                else
                    OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
