using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO plateKitchenObjectSO;
    private readonly int maxSpawnedPlates = 4;
    private int currentSpawnedPlates;
    private float currentSpawnTimer;
    private readonly float spawnCooldown = 4.5f;

    public event Action OnSpawnPlate;
    public event Action OnPickupPlate;

    private void Update()
    {
        currentSpawnTimer += Time.deltaTime;
        if (currentSpawnTimer >= spawnCooldown)
        {
            currentSpawnTimer = 0;
            if (currentSpawnedPlates < maxSpawnedPlates)
            {
                currentSpawnedPlates++;
                OnSpawnPlate?.Invoke();
            }
        }
    }
    public override void Interact(Player player)
    {
        if (player.GetKitchenObject() == null && currentSpawnedPlates > 0)
        {
            KitchenObject.Instantiate(plateKitchenObjectSO,player);
            OnPickupPlate?.Invoke();
            currentSpawnedPlates--;
        }
    }
}
