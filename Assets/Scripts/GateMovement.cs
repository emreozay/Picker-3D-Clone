using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMovement : MonoBehaviour
{
    private float signOfAngle;
    private float angle;
    private bool isGatesUp;

    void Start()
    {
        ContainerControl.containerPass += LiftGates;

        signOfAngle = Mathf.Sign(transform.localPosition.x);

        if (signOfAngle == 1)
            angle = 60f;
        else
            angle = 300f;
    }

    void Update()
    {
        if (isGatesUp && (transform.localRotation.eulerAngles.y * signOfAngle < angle * signOfAngle || transform.localRotation.eulerAngles.y == 0f))
            transform.RotateAround(transform.parent.position, Vector3.forward, 30 * signOfAngle * Time.deltaTime);

        if (isGatesUp && transform.localRotation.eulerAngles.y * signOfAngle >= angle * signOfAngle)
        {
            if(ContainerControl.gatesUp != null)
            {
                ContainerControl.gatesUp();
                isGatesUp = false;
            }
        }
    }

    private void LiftGates()
    {
        isGatesUp = true;
    }
}
