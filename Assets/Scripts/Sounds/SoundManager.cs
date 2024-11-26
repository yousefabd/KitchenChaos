using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClipRefsSO audioClips;
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
        DeliveryCounter.OnDeliverySuccess += DeliveryCounter_OnDeliverySuccess;
        DeliveryCounter.OnDeliveryFailed += DeliveryCounter_OnDeliveryFailed;
        BaseCounter.OnPutDownObject += BaseCounter_OnPutDownObject;
        Player.Instance.OnPickupObject += Player_OnPickupObject;
        TrashCounter.OnTrash += TrashCounter_OnTrash;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.chop, (sender as CuttingCounter).transform.position);
    }

    private void TrashCounter_OnTrash(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.trash, (sender as TrashCounter).transform.position);
    }

    private void Player_OnPickupObject(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.objectPickup, (sender as Player).transform.position);
    }

    private void BaseCounter_OnPutDownObject(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.objectDrop, (sender as BaseCounter).transform.position);
    }
    private void DeliveryCounter_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.deliveryFail, (sender as DeliveryCounter).transform.position);
    }

    private void DeliveryCounter_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.deliverySuccess, (sender as DeliveryCounter).transform.position);
    }

    private void PlaySound(AudioClip[] audioClipsArray, Vector3 position, float volume = 2.0f)
    {
        AudioSource.PlayClipAtPoint(audioClipsArray[Random.Range(0, audioClipsArray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 2.0f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootStepsSound(Vector3 position,float volume)
    {
        PlaySound(audioClips.footStep, position, volume);
    }
}
