using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float forceSpeed = 3f;
    private float lastMousePoint;
    private bool isMouseDown = false;
    private Rigidbody rb;
    private bool isStop = false;
    private List<GameObject> collectedObjects;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collectedObjects = new List<GameObject>();

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            collectedObjects.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            collectedObjects.Remove(collision.gameObject);
        }
    }

    private void StopMovement()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        isStop = true;

        for (int i = 0; i < collectedObjects.Count; i++)
        {
            collectedObjects[i].GetComponent<Rigidbody>().AddForce(Vector3.forward * forceSpeed, ForceMode.Impulse);
        }
    }

    private void ContinueMovement()
    {
        isStop = false;
    }
}
