using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button exit;

    private void Awake()
    {
        play.onClick.AddListener(OnPLayClicked);
        exit.onClick.AddListener(() => {
            Application.Quit();
        });
    }
    private void OnPLayClicked()
    {
        Loader.Load(Loader.Scene.Loading);
    }
}
