using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveOnVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject[] StoveVisualArray;
    private void Start()
    {
        stoveCounter.OnCook += StoveCounter_OnCook;
    }

    private void StoveCounter_OnCook(bool obj)
    {
        foreach (var item in StoveVisualArray)
        {
            item.SetActive(obj);
        }
    }
}
