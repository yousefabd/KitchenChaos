using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCountDownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        gameObject.SetActive(GameManager.Instance.IsCountDownActive());
    }
    private void Update()
    {
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetCountDownTimer()).ToString();
    }
}
