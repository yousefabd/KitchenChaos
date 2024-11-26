using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] StoveCounter stoveCounter;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        stoveCounter.OnCook += StoveCounter_OnCook;
    }

    private void StoveCounter_OnCook(bool obj)
    {
        if (obj)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
