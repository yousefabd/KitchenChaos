using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingAnimator : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_onCut;
    }

    private void CuttingCounter_onCut()
    {
        animator.SetTrigger("Cut");
    }
}
