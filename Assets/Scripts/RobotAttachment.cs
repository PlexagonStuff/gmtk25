using UnityEngine;

public class RobotAttachment : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField]
    protected float topSpeed = 5f;
    [SerializeField]
    protected float accFactor = 0.1f;
    [SerializeField]
    protected float dragFactor = 0.2f;
    [SerializeField]
    protected float movementFuelPerSec = 5f;

    public virtual void RightArrow()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, topSpeed, accFactor);
        transform.parent.GetComponent<RobotController>().Fuel -= movementFuelPerSec * Time.fixedDeltaTime;
    }

    public virtual void LeftArrow()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, -1*topSpeed, accFactor); 
        transform.parent.GetComponent<RobotController>().Fuel -= movementFuelPerSec * Time.fixedDeltaTime;
    }

    public virtual void UpArrow()
    {

    }

    public virtual void Idle()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 0, dragFactor);
    }
}
