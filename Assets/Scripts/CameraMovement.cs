using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool isCameraStop;

    private void Start()
    {
        ContainerControl.containerStop += StopCameraMovement;
        ContainerControl.gatesUp += ContinueCameraMovement;
    }

    private void LateUpdate()
    {
        if(!isCameraStop)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f * Time.deltaTime);
    }

    private void StopCameraMovement()
    {
        isCameraStop = true;
    }

    private void ContinueCameraMovement()
    {
        isCameraStop = false;
    }
}
