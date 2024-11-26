using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private readonly float walkTimerMax = 0.2f;
    private float currentwalkTimer;
    private void Update()
    {
        currentwalkTimer -= Time.deltaTime;
        if (currentwalkTimer < 0f ) {
            currentwalkTimer = walkTimerMax;
            if (Player.Instance.IsWalking())
            {
                SoundManager.Instance.PlayFootStepsSound(Player.Instance.transform.position, 1f);
            }
        }
    }
}
