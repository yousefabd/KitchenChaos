using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button resume;
    [SerializeField] Button mainMenu;

    private void Awake()
    {
        resume.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePause();
            gameObject.SetActive(false);
        });
        mainMenu.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenu);
            GameManager.Instance.TogglePause();
        });
    }
    private void Start()
    {
        InputManager.Instance.OnPause += InputManager_OnPause;
        gameObject.SetActive(false);
    }

    private void InputManager_OnPause()
    {
        gameObject.SetActive(GameManager.Instance.paused);
    }
}
