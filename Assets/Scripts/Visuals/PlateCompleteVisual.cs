using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct KitchenObjectSO_GameObject
{
    public KitchenObjectSO kitchenObjectSO;
    public GameObject gameObject;
}
public class PlateCompleteVisual : MonoBehaviour
{
    
    [SerializeField] private List<KitchenObjectSO_GameObject> ingredients;
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;
        foreach(KitchenObjectSO_GameObject element in ingredients)
        {
            element.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnAddIngredient(KitchenObjectSO obj)
    {
        foreach (KitchenObjectSO_GameObject element in ingredients)
        {
            if(element.kitchenObjectSO == obj)
            {
                element.gameObject.SetActive(true);
            }
        }
    }
}
