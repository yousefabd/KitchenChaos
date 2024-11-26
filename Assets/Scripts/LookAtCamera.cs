using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum ViewSettings
{
    LookAtCamera,LookAtCameraInverted,LookAtCameraForward,LookAtCameraForwardInverted
}
public class LookAtCamera : MonoBehaviour
{
    [SerializeField] ViewSettings view;
    private void LateUpdate()
    {
        switch (view)
        {
            case ViewSettings.LookAtCamera:
                transform.LookAt(Camera.main.transform);
                break;
            case ViewSettings.LookAtCameraInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case ViewSettings.LookAtCameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case ViewSettings.LookAtCameraForwardInverted:
                transform.forward= -1*Camera.main.transform.forward;
                break;
        }
    }
}
