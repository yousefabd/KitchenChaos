using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSpawnerVisual : MonoBehaviour
{
    [SerializeField] Transform counterTop;
    [SerializeField] PlateCounter plateCounter;
    [SerializeField] Transform PlateVisual;

    private List<GameObject> platesList;
    private void Awake()
    {
        platesList = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnSpawnPlate += PlateCounter_OnSpawnPlate;
        plateCounter.OnPickupPlate += PlateCounter_OnPickupPlate;
    }

    private void PlateCounter_OnPickupPlate()
    {
        GameObject topPlate = platesList[^1];
        platesList.Remove(topPlate);
        Destroy(topPlate);
    }

    private void PlateCounter_OnSpawnPlate()
    {

        Transform plateVisualTransform = Instantiate(PlateVisual, counterTop);
        plateVisualTransform.localPosition = new Vector3(0f, 0.1f * platesList.Count);
        platesList.Add(plateVisualTransform.gameObject);
    }
}
