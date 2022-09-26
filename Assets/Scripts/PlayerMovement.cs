using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 1;
    private float lastMousePoint;
    private bool isMouseDown = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            lastMousePoint = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown= false;
        }

        if (isMouseDown)
        {
            float difference = Input.mousePosition.x - lastMousePoint;
            transform.position = new Vector3(transform.position.x + difference * Time.deltaTime * speed, transform.position.y, transform.position.z);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.4f, 1.4f), transform.position.y, transform.position.z);
            lastMousePoint = Input.mousePosition.x;
        }
    }
}
