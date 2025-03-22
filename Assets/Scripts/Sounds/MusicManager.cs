using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    private float musicVolume = 0.5f;
    private readonly float musicVolumeMax = 1.0f;
    public static MusicManager Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        
        audioSource = GetComponent<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        audioSource.volume = musicVolume;
    }
    public void IncreaseVolume(float increment) {
        musicVolume = Mathf.Min(musicVolume + musicVolumeMax * increment, musicVolumeMax);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        audioSource.volume = musicVolume;
    }
    public void DecreaseVolume(float increment) {
        musicVolume = Mathf.Max(musicVolume - musicVolumeMax * increment, 0f);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        audioSource.volume = musicVolume;
    }
    public  float GetVolumeNormalized() {
        return musicVolume / musicVolumeMax;
    }
}
