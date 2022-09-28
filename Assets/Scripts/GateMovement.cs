using UnityEngine;

public class GateMovement : MonoBehaviour
{
    [SerializeField] private float gateSpeed = 50f;
    private float signOfAngle;
    private float angle;
    private bool isGatesUp;

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
            }
        }
    }

    private void LiftGates()
    {
        isGatesUp = true;
    }
}
