using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    private IKitchenObjectParent kitchenObjectParent;
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //set old clear counter to null kitchen object
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        //set new clear counter to the given kitchen object
        if(kitchenObjectParent.GetKitchenObject() != null)
        {
            Debug.LogError("Another instance of type "+ kitchenObjectParent.GetKitchenObject().name+" Already exists on the counter "+kitchenObjectParent);
        }
        kitchenObjectParent.SetKitchenObject(this);
        //change position of kitchen object visually
        transform.parent = kitchenObjectParent.GetObjectSpawnPoint();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent KitchenObjectParent { get { return kitchenObjectParent; } }
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
    public static KitchenObject Instantiate(KitchenObjectSO kitchenObjectSO,IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, kitchenObjectParent.GetObjectSpawnPoint());
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenOject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenOject = this as PlateKitchenObject;
            return true;
        }
        plateKitchenOject = null;
        return false;
    }

}
