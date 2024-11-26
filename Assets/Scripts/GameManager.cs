using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class GameManager : MonoBehaviour
{
    private enum State
    {
        WaitToStart,
        CountToStart,
        GamePlaying,
        GameOver
    }
    private State state;
    public bool paused = false;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        state = State.WaitToStart;
    }
    private void Start()
    {
        InputManager.Instance.OnPause += InputManager_OnPause;
    }

    private void InputManager_OnPause()
    {
        TogglePause();
    }
    public void TogglePause()
    {
        paused = !paused;
        if (!paused)
        {
            Time.timeScale = 0f;
        }
        else Time.timeScale = 1f;
    }
    private float waitToStartCounter = 1f;
    private float countToStartCounter = 3f;
    private float gamePlayingCounter = 60f;
    private float gamePlayingCounterMax = 60f;

    public event EventHandler OnStateChanged;
    private void Update()
    {
        switch(state)
        {
            case State.WaitToStart:
                waitToStartCounter -= Time.deltaTime;
                if (waitToStartCounter <= 0f)
                {
                    state = State.CountToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountToStart:
                countToStartCounter -= Time.deltaTime;
                if (countToStartCounter <= 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingCounter -= Time.deltaTime;
                if (gamePlayingCounter <= 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }
    public bool IsGamePlaying()
    {
        return state==State.GamePlaying;
    }
    public bool IsCountDownActive()
    {
        return state == State.CountToStart;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public float GetCountDownTimer()
    {
        return countToStartCounter;
    }
    public float GetCountTimeNormalized()
    {
        return gamePlayingCounter / gamePlayingCounterMax;
    }
}
