using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private void Start()
    {
        ContainerControl.containerStop += AddForceToObstacles;
    }

    private void AddForceToObstacles()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 3f, ForceMode.Impulse);
    }
}
