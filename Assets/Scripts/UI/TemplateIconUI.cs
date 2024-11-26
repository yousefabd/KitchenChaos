using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemplateIconUI : MonoBehaviour
{
    [SerializeField] private Image iconSprite;

    public void SetIconSprite(KitchenObjectSO kitchenObjectSO)
    {
        iconSprite.sprite = kitchenObjectSO.sprite;
    }
}
