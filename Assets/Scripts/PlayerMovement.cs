﻿using UnityEngine;

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
        CheckInput();
    }

    private void FixedUpdate()
    {
        if (!isStop)
        {
            MovePlayer();
        }
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            lastMousePoint = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
    }

    private void MovePlayer()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spinner"))
        {
            Destroy(other.gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void StopMovement()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        isStop = true;
    }

    private void ContinueMovement()
    {
        isStop = false;
    }
}
