using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMovement : MonoBehaviour
{
    private float signOfAngle;
    private float angle;

    void Start()
    {
        signOfAngle = Mathf.Sign(transform.localPosition.x);

        if (signOfAngle == 1)
            angle = 60f;
        else
            angle = 300f;
    }

    void Update()
    {
        if (transform.localRotation.eulerAngles.y * signOfAngle < angle * signOfAngle || transform.localRotation.eulerAngles.y == 0f)
            transform.RotateAround(transform.parent.position, Vector3.forward, 20 * signOfAngle * Time.deltaTime);
    }
}
