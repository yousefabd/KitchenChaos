using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeManagerUI : MonoBehaviour
{
    [SerializeField] private Orders ordersObj;
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private TextMeshProUGUI orderTitle;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        Orders.Instance.OnAddOrder += OrdersObj_OnAddOrder;
        Orders.Instance.OnSubmitOrder += OrdersObj_OnSubmitOrder;
    }

    private void OrdersObj_OnSubmitOrder()
    {
        UpdateVisuals();
    }

    private void OrdersObj_OnAddOrder()
    {
        Debug.Log(this == null);
        UpdateVisuals();
    }
    private void UpdateVisuals() {
        ClearVisuals();
        foreach (OrderSO orderSO in Orders.Instance.GetCurrentOrders())
        {
            orderTitle.text = orderSO.name;
            Transform recipeTransform = Instantiate(recipeTemplate, transform);
            recipeTransform.GetComponent<RecipeTemplateUI>().SetIngredients(orderSO.GetRecipeOrders());
            recipeTransform.gameObject.SetActive(true);
        }
    }
    private void ClearVisuals()
    {
        foreach (Transform child in transform)
        {
            if (child != recipeTemplate)
            {
                 Destroy(child.gameObject);
            }
        }
    }
}
