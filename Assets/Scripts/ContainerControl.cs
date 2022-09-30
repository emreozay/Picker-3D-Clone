using System;
using System.Collections;
using UnityEngine;

public class ContainerControl : MonoBehaviour
{
    [SerializeField] private Color containerPassColor;
    [SerializeField] private float containerUpSpeed = 3f;
    private Vector3 targetPosition;
    private int sphereCount;
    private bool isUp;
    private bool isTrigger;

    public static Action containerStop;
    public static Action containerPass;
    public static Action gatesUp;

    public static int index = 0;

    private float timer = 0.0f;

    private void Start()
    {
        targetPosition = new Vector3(transform.position.x, -0.165f, transform.position.z);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isUp)
        {
            MoveContainerUp();
        }
    }

    private void MoveContainerUp()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * containerUpSpeed);

        if (transform.position.y >= -0.166f)
        {
            isUp = false;
            if (containerPass != null)
            {
                print("WORKED!");
                ContainerControl.index++;
                containerPass();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Collectable"))
        {
            sphereCount++;
            Destroy(collision.gameObject);
        }

        if (sphereCount >= 10 && timer >= 5f)
        {
            isUp = true;
            transform.GetComponent<Renderer>().material.color = containerPassColor;

            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && !isTrigger)
        {
            if (containerStop != null)
                containerStop();

            StartCoroutine(SetTriggerOnAndOff());
        }
    }

    private IEnumerator SetTriggerOnAndOff()
    {
        isTrigger = true;
        yield return new WaitForSeconds(5f);
        isTrigger = false;
    }
}
