using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class RobotJetpack : RobotAttachment
{
    public float pressure = 0;
    public float rotPressure = 1;
    private double prevDirection = 0;
    public float rotationalChange = (float)0.22;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void RightArrow()
    {
        HandleRotation(-1);
    }

    public override void LeftArrow()
    {
        HandleRotation(1);
    }

    void HandleRotation(float direction)
    {
        if (direction != prevDirection)
        {
            rotPressure = 1;
        }
        rotPressure += (float)0.01;
        Debug.Log("Is this getting called?");
        var impulse = (rotationalChange * Mathf.Deg2Rad * direction * rotPressure * rb.inertia);
        rb.AddTorque(impulse, ForceMode2D.Impulse);
        prevDirection = direction;
        GetComponentInParent<RobotController>().Fuel -= (float)((rotPressure - 1) * 0.5);
        
    }

    public override void UpArrow()
    {
        pressure += (float)0.005;
        rb.AddForce(rb.transform.up * pressure, ForceMode2D.Impulse);
        GetComponentInParent<RobotController>().Fuel -= (float)((pressure) * 0.5);
    }

    public override void Idle()
    {
        pressure = 0;
    }
}

