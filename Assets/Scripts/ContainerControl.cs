using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerControl : MonoBehaviour
{
    [SerializeField] private Color containerPassColor;
    private Vector3 targetPosition;
    private int sphereCount;
    private bool isUp;

    public static Action containerStop;
    public static Action containerPass;
    public static Action gatesUp;

    private void Start()
    {
        targetPosition = new Vector3(transform.position.x, -0.165f, transform.position.z);
    }

    private void Update()
    {
        if (isUp)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2f);
            
            if (transform.position.y >= -0.166f)
            {
                isUp = false;
                if (containerPass != null)
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

        if (sphereCount >= 10)
        {
            isUp = true;
            transform.GetComponent<Renderer>().material.color = containerPassColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (containerStop != null)
                containerStop();
        }
    }
}
