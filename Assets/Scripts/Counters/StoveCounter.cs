using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
enum State
{
    Idle,
    Cooking,
    Cooked,
    OverCooked
}
public class StoveCounter : BaseCounter,IHasProgress
{
    private State currentState;
    [SerializeField] private RecipeSO cookingRecipe;
    [SerializeField] private RecipeSO burningRecipe;
    [SerializeField] private GameObject progressBar;
    private float cookingTimer;
    public event Action<float> OnProgressChanged;
    public event Action<bool> OnCook;
    
    private void Start()
    {
        currentState = State.Idle;
    }
    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                progressBar.SetActive(false);
                OnCook?.Invoke(false);
                break;
            case State.Cooking:
                progressBar.SetActive(true);
                cookingTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(cookingTimer/cookingRecipe.maxBound);
                if (cookingTimer >= cookingRecipe.maxBound)
                {
                    currentState = State.Cooked;
                    GetKitchenObject().DestroySelf();
                    KitchenObject.Instantiate(cookingRecipe.output, this);
                    cookingTimer = 0;
                }
                break;
            case State.Cooked:
                cookingTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(cookingTimer/burningRecipe.maxBound);
                if (cookingTimer >= burningRecipe.maxBound)
                {
                    currentState = State.OverCooked;
                    GetKitchenObject().DestroySelf();
                    KitchenObject.Instantiate(burningRecipe.output, this);
                    cookingTimer = 0;
                }
                break;
            case State.OverCooked:
                OnCook?.Invoke(false);
                progressBar.SetActive(false);
                break;
        }
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
            }
        }
        //make player pick up that object
        else if (player.GetKitchenObject() == null)
        {
            currentState = State.Idle;
            GetKitchenObject().SetKitchenObjectParent(player);
        }
        //both counter and player are holding an object
        else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
            {
                GetKitchenObject().DestroySelf();
                currentState = State.Idle;
            }
        }
    }
    public bool FindRecipe(KitchenObjectSO input)
    {
        if (input == cookingRecipe.input)
        {
            currentState = State.Cooking;
            OnCook?.Invoke(true);
            cookingTimer = 0;
            return true;
        }
        return false;
    }
}
