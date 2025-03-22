using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCountDownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownText;
    private Animator textAnimator;
    private int currentTimerCeiled = 4;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        textAnimator = countDownText.GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        gameObject.SetActive(GameManager.Instance.IsCountDownActive());
    }
    private void Update()
    {
        int timeCeiled = (int) Mathf.Ceil(GameManager.Instance.GetCountDownTimer());
        if(timeCeiled != currentTimerCeiled) {
            currentTimerCeiled = timeCeiled;
            countDownText.text = currentTimerCeiled.ToString();
            textAnimator.Play("CountDown",-1,0);
        }
    }
}
