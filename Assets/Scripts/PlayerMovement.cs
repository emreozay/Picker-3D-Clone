using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private float lastMousePoint;
    private bool isMouseDown = false;
    private Rigidbody rb;
    private bool isStop = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        ContainerControl.containerStop += StopMovement;
        ContainerControl.gatesUp += ContinueMovement;
    }

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
    }

    private void FixedUpdate()
    {
        if (!isStop)
        {
            if (isMouseDown)
            {
                float difference = Input.mousePosition.x - lastMousePoint;

                float xPos = transform.position.x + difference * Time.deltaTime * speed;
                xPos = Mathf.Clamp(xPos, -1.4f, 1.4f);

                rb.MovePosition(new Vector3(xPos, transform.position.y, transform.position.z + 3f * Time.fixedDeltaTime));

                lastMousePoint = Input.mousePosition.x;
            }
            else
            {
                rb.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f * Time.fixedDeltaTime));
            }
        }
    }

    private void StopMovement()
    {
        isStop = true;
    }

    private void ContinueMovement()
    {
        isStop = false;
    }
}
