using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        //if counter is empty
        if (GetKitchenObject() == null)
        {
            //player is holding an object
            if (player.GetKitchenObject() != null)
            {
                //put that object on the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        //make player pick up that object
        else if(player.GetKitchenObject() == null)
        {

            GetKitchenObject().SetKitchenObjectParent(player);
        }
        //both counter and player are holding an object
        else if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
            {
                GetKitchenObject().DestroySelf();
            }
        }
        else if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
        
    }
}
