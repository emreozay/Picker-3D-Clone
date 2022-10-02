using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float xSpeed = 0.5f;
    [SerializeField] private float forceSpeed = 2.5f;
    private float moveForwardSpeed = 4.5f;
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

            float xPos = transform.position.x + difference * Time.deltaTime * xSpeed;
            xPos = Mathf.Clamp(xPos, -1.4f, 1.4f);

            rb.MovePosition(new Vector3(xPos, transform.position.y, transform.position.z + moveForwardSpeed * Time.fixedDeltaTime));

            lastMousePoint = Input.mousePosition.x;
        }
        else
        {
            rb.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveForwardSpeed * Time.fixedDeltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spinner"))
        {
            Destroy(other.gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if (other.CompareTag("Collectable"))
        {
            collectedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            collectedObjects.Remove(other.gameObject);
        }
    }

    private void StopMovement()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        
        isStop = true;

        for (int i = 0; i < collectedObjects.Count; i++)
        {
            GameObject obj = collectedObjects[i];

            if (obj != null)
            {
                obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1f, 1f) * forceSpeed, ForceMode.Impulse);
            }
        }

        collectedObjects.Clear();
    }

    private void ContinueMovement()
    {
        isStop = false;
    }

    private void OnDestroy()
    {
        ContainerControl.containerStop -= StopMovement;
        ContainerControl.gatesUp -= ContinueMovement;
    }
}
