using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticFields : MonoBehaviour
{
    private void Awake()
    {
        TrashCounter.ResetStaticFields();
        DeliveryCounter.ResetStaticFields();
        CuttingCounter.ResetStaticFields();
        BaseCounter.ResetStaticFields();
        Orders.ResetStaticFields();
    }
}
