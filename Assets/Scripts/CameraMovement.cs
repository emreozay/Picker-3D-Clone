﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5f * Time.deltaTime);
    }
}