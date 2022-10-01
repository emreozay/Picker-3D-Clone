using UnityEngine;

public class GateMovement : MonoBehaviour
{
    private float gateSpeed = 70f;
    private float signOfAngle;
    private float angle;

    private bool isGatesUp;
    private bool isThisGate;

    void Start()
    {
        ContainerControl.containerPass += LiftGates;

        signOfAngle = Mathf.Sign(transform.localPosition.x);

        if (signOfAngle == 1)
            angle = 60f;
        else
            angle = 300f;
    }

    void Update()
    {
        OpenGates();

        GatesFullyOpen();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isThisGate = true;
    }

    private void OpenGates()
    {
        bool shouldOpen = isGatesUp && (transform.localRotation.eulerAngles.y * signOfAngle < angle * signOfAngle || transform.localRotation.eulerAngles.y == 0f);

        if (shouldOpen)
            transform.RotateAround(transform.parent.position, Vector3.forward, gateSpeed * signOfAngle * Time.deltaTime);
    }

    private void GatesFullyOpen()
    {
        bool fullyOpen = isGatesUp && transform.localRotation.eulerAngles.y * signOfAngle >= angle * signOfAngle;

        if (fullyOpen)
        {
            if (ContainerControl.gatesUp != null)
            {
                ContainerControl.gatesUp();
                isGatesUp = false;
                isThisGate = false;
            }
        }
    }

    private void LiftGates()
    {
        if(isThisGate)
            isGatesUp = true;
    }
}
