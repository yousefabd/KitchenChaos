using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validIngredients;
    private List<KitchenObjectSO> currentIngredients;
    public event Action<KitchenObjectSO> OnAddIngredient;
    private void Awake()
    {
        currentIngredients = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO ingredient)
    {
        if(currentIngredients.Contains(ingredient) || !validIngredients.Contains(ingredient)) return false;
        currentIngredients.Add(ingredient);
        OnAddIngredient?.Invoke(ingredient);
        return true;
    }
    public List<KitchenObjectSO> GetRecipe()
    {
        return currentIngredients;
    }
}
