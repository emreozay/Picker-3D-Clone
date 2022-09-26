using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private float lastMousePoint;
    private bool isMouseDown = false;
    private Rigidbody rb;
    private float playerY = -0.0009999424f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        
        /*if (isMouseDown)
        {
            float difference = Input.mousePosition.x - lastMousePoint;
            transform.position = new Vector3(transform.position.x + difference * Time.deltaTime * speed, playerY, transform.position.z);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.4f, 1.4f), playerY, transform.position.z);
            
            lastMousePoint = Input.mousePosition.x;
        }

       transform.position = new Vector3(transform.position.x, playerY, transform.position.z + 3f * Time.deltaTime);*/
    }

    private void FixedUpdate()
    {
        if (isMouseDown)
        {
            float difference = Input.mousePosition.x - lastMousePoint;
            //transform.position = new Vector3(transform.position.x + difference * Time.deltaTime * speed, playerY, transform.position.z);
            
            float xPos = transform.position.x + difference * Time.deltaTime * speed;
            xPos = Mathf.Clamp(xPos, -1.4f, 1.4f);

            rb.MovePosition(new Vector3(xPos, playerY, transform.position.z + 3f * Time.fixedDeltaTime));

            lastMousePoint = Input.mousePosition.x;
        }
        else
        {
            rb.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f * Time.fixedDeltaTime));

        }
    }
}
