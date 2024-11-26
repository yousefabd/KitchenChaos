using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    [SerializeField] private RecipeSO[] cuttingRecipeArray;
    [SerializeField] private GameObject progressBar;
    private KitchenObjectSO slicedKitchenObjectSO;
    private int maxSlices;
    private int currentSliceProgress;
    public event Action<float> OnProgressChanged;
    public event Action OnCut;
    public static event EventHandler OnAnyCut;

    new public static void ResetStaticFields()
    {
        OnAnyCut = null;
    }
    public override void Interact(Player player)
    {
        //if counter is empty
        if (GetKitchenObject() == null)
        {
            //player is holding an object
            if (player.GetKitchenObject() != null && FindRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                //put that object on the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
                OnProgressChanged?.Invoke(0f);
                progressBar.SetActive(true);
            }
        }
        //make player pick up that object
        else if (player.GetKitchenObject() == null)
        {
            progressBar.SetActive(false);
            GetKitchenObject().SetKitchenObjectParent(player);
        }
        //both counter and player are holding an object
        else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
            {
                progressBar.SetActive(false);
                GetKitchenObject().DestroySelf();
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if(GetKitchenObject()!=null)
        {
            if (FindRecipe(GetKitchenObject().GetKitchenObjectSO()))
            {
                currentSliceProgress++;
                OnProgressChanged?.Invoke((float)currentSliceProgress / maxSlices);
                OnCut?.Invoke();
                OnAnyCut?.Invoke(this, EventArgs.Empty);
            }
            if (currentSliceProgress >= maxSlices)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.Instantiate(slicedKitchenObjectSO, this);
                currentSliceProgress = 0;
            }
        }
    }
    public bool FindRecipe(KitchenObjectSO input)
    {
        foreach(RecipeSO recipe in cuttingRecipeArray)
        {
            if(recipe.input == input)
            {
                slicedKitchenObjectSO = recipe.output;
                maxSlices = recipe.maxBound;
                return true;
            }
        }
        return false;
    }
}
