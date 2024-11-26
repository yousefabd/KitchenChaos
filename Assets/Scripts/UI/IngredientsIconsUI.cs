using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsIconsUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;
    }

    private void PlateKitchenObject_OnAddIngredient(KitchenObjectSO ingredient)
    {
        Transform iconTranform=Instantiate(iconTemplate, transform);
        iconTranform.GetComponent<TemplateIconUI>().SetIconSprite(ingredient);
        iconTranform.gameObject.SetActive(true);
    }
}
