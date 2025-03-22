using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClipRefsSO audioClips;
    public static SoundManager Instance { get; private set; }

    private float soundVolume = 2.0f;
    private readonly float soundVolumeMax = 4.0f;
    private void Awake()
    {
        Instance = this;
        soundVolume = PlayerPrefs.GetFloat("soundVolume", 2.0f);
        PlayerPrefs.DeleteAll();
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

    private void PlaySound(AudioClip[] audioClipsArray, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClipsArray[Random.Range(0, audioClipsArray.Length)], position, soundVolume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, soundVolume);
    }

    public void PlayFootStepsSound(Vector3 position)
    {
        PlaySound(audioClips.footStep, position);
    }

    public void IncreaseVolume(float increment) {
        soundVolume = Mathf.Min(soundVolume + soundVolumeMax * increment, soundVolumeMax);
        PlayerPrefs.SetFloat("soundVolume",soundVolume);
    }
    public void DecreaseVolume(float increment) {
        soundVolume = Mathf.Max(soundVolume - soundVolumeMax * increment, 0f);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
    }
    public float GetVolumeNormalized() {
        Debug.Log(soundVolume);
        return soundVolume / soundVolumeMax;
    }
}
