using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ContainerControl : MonoBehaviour
{
    [SerializeField] private Color containerPassColor;
    private float containerUpSpeed = 5f;
    private Vector3 targetPosition;
    private int sphereCount;
    private bool isUp;
    private bool isTrigger;
    private bool isLose;

    public static Action containerStop;
    public static Action containerPass;
    public static Action gatesUp;

    private float timer = 0.0f;

    private TextMeshPro textMesh;
    private int objectAmount;


    private void Start()
    {
        targetPosition = new Vector3(transform.position.x, -0.165f, transform.position.z);

        textMesh = GetComponentInChildren<TextMeshPro>();
        objectAmount = int.Parse(textMesh.text.Substring(3));
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

            if (textMesh != null)
                textMesh.text = sphereCount + " / " + objectAmount;
        }

        if (sphereCount >= objectAmount && timer >= 5f)
        {
            isUp = true;
            transform.GetComponent<Renderer>().material.color = containerPassColor;

            if (textMesh != null)
                Destroy(textMesh, 0.5f);

            timer = 0f;
        }
    }

    private IEnumerator CheckIfLose()
    {
        yield return new WaitForSeconds(2f);

        if(sphereCount < objectAmount)
        {
            if (UIController.levelFailed != null)
                UIController.levelFailed();

            isLose = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && !isTrigger)
        {
            if (containerStop != null)
                containerStop();

            if (!isLose)
                StartCoroutine(CheckIfLose());

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
