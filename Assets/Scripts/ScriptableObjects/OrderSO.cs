using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class OrderSO : ScriptableObject
{
    [SerializeField] List<KitchenObjectSO> RecipeOrder;

    public List<KitchenObjectSO> GetRecipeOrders()
    {
        return RecipeOrder;
    }
    public void SetRecipeOrders(List<KitchenObjectSO> RecipeOrders) { 
        this.RecipeOrder = RecipeOrders;
    }
}
