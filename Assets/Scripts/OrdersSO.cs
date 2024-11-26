using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class Orders : ScriptableObject 
{
    [SerializeField] private List<OrderSO> ordersList;
    private List<OrderSO> currentOrders;
    private int maxOrdersCount = 3;

    public event Action OnAddOrder;
    public event Action OnSubmitOrder;

    public static Orders Instance {  get; private set; }

    public static void ResetStaticFields()
    {
        Instance.OnAddOrder = null;
        Instance.OnSubmitOrder = null;
    }
    private void Awake()
    {
        Instance = this;
        currentOrders = new List<OrderSO>();
    }

    public void AddNewOrder()
    {
        if (currentOrders.Count < maxOrdersCount)
        {
            OrderSO newOrder = ordersList[UnityEngine.Random.Range(0, ordersList.Count)];
            currentOrders.Add(newOrder);
            OnAddOrder?.Invoke();
        }
    }
    public bool SubmitOrder(OrderSO newOrder)
    {
        foreach (OrderSO order in currentOrders)
        {
            if (order.GetRecipeOrders().Count == newOrder.GetRecipeOrders().Count)
            {
                bool foundRecipe = true;
                foreach (KitchenObjectSO newIngredient in newOrder.GetRecipeOrders())
                {
                    bool foundIngredient = false;
                    foreach (KitchenObjectSO ingredient in order.GetRecipeOrders())
                    {
                        if (ingredient == newIngredient)
                        {
                            foundIngredient = true;
                        }
                    }
                    if (!foundIngredient)
                    {
                        foundRecipe = false;
                    }
                }
                if(foundRecipe)
                {
                    currentOrders.Remove(order);
                    OnSubmitOrder?.Invoke();
                    return true;
                }
            }
        }
        Debug.Log("Couldn't Find Matching Order");
        return false;
    }
    public void SetMaxOrdersCount(int maxOrdersCount)
    {
        this.maxOrdersCount = maxOrdersCount;
    }
    public void PrintOrders()
    {
        foreach (var order in currentOrders)
        {
            Debug.Log(order);
        }
    } 

    public void ClearOrders()
    {
        currentOrders.Clear();
    }
    public List<OrderSO> GetCurrentOrders()
    {
        return currentOrders;
    }
}
