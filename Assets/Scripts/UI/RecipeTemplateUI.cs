using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTemplateUI : MonoBehaviour {
    [SerializeField] Transform iconTemplate;
    [SerializeField] Transform iconContainer;
    private List<KitchenObjectSO> ingredients;
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetIngredients(List<KitchenObjectSO> ingredients)
    {
        this.ingredients = ingredients;
        UpdateVisuals();
    }
    public void UpdateVisuals()
    {
        ClearVisuals();
        foreach (KitchenObjectSO ingredient in ingredients) {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.GetComponent<Image>().sprite = ingredient.sprite;
            iconTransform.gameObject.SetActive(true);
        }
    }
    public void ClearVisuals()
    {
        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
    }
}
