using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerAnimator : MonoBehaviour
{
    [SerializeField] ContainerCounter containerCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnGrabObject += ContainerCounter_onGrabObject;
    }

    private void ContainerCounter_onGrabObject()
    {
        animator.SetTrigger("OpenClose");
    }
}
