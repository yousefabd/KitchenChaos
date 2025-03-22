using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button sfxMinus;
    [SerializeField] private Image sfx;
    [SerializeField] private Button sfxPlus;

    [SerializeField] private Button musicMinus;
    [SerializeField] private Image music;
    [SerializeField] private Button musicPlus;

    [SerializeField] private Button options;
    [SerializeField] private Button back;

    private void Start() {
        sfx.fillAmount = SoundManager.Instance.GetVolumeNormalized();
        music.fillAmount = MusicManager.Instance.GetVolumeNormalized();
        options.onClick.AddListener(() => {
            gameObject.SetActive(true);
        });
        back.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
        sfxMinus.onClick.AddListener(() => {
            float sfxVolume = SoundManager.Instance.GetVolumeNormalized();
            SoundManager.Instance.DecreaseVolume(0.1f);
            sfx.fillAmount = SoundManager.Instance.GetVolumeNormalized();
        });
        sfxPlus.onClick.AddListener(() => {
            float sfxVolume = SoundManager.Instance.GetVolumeNormalized();
            SoundManager.Instance.IncreaseVolume(0.1f);
            sfx.fillAmount = SoundManager.Instance.GetVolumeNormalized();
        });
        musicMinus.onClick.AddListener(() => {
            float musicVolume = MusicManager.Instance.GetVolumeNormalized();
            MusicManager.Instance.DecreaseVolume(0.1f);
            music.fillAmount = MusicManager.Instance.GetVolumeNormalized();
        });
        musicPlus.onClick.AddListener(() => {
            float musicVolume = MusicManager.Instance.GetVolumeNormalized();
            MusicManager.Instance.IncreaseVolume(0.1f);
            music.fillAmount = MusicManager.Instance.GetVolumeNormalized();
        });
        gameObject.SetActive(false);
    }
}
