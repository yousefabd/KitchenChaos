using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private BaseCounter progressCounter;
    private IHasProgress hasProgress;
    private void Start()
    {
        hasProgress=progressCounter.GetComponent<IHasProgress>();
        if(hasProgress == null ) { 
            Debug.LogError("instance "+progressCounter.name+" can not have progress bar.");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
    }

    private void HasProgress_OnProgressChanged(float obj)
    {
        barImage.fillAmount = obj;
    }
}
