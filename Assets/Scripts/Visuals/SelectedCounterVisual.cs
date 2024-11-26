using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter baseCounter;
    [SerializeField] GameObject[] HilightVisuals;

    private void Start()
    {
        Player.Instance.OnChangedSelectedCounter += Player_OnChangedSelectedCounter;
    }

    private void Player_OnChangedSelectedCounter(BaseCounter obj)
    {
        if (obj == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }

    }
    private void Show()
    {
        foreach (GameObject hilightVisual in HilightVisuals)
        {
            hilightVisual.SetActive(true);
        }
    }
    private void Hide() {
        foreach (GameObject hilightVisual in HilightVisuals)
        {
            hilightVisual.SetActive(false);
        }
    }
}
