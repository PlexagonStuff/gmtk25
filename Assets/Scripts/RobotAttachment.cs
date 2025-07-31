using UnityEngine;

public class RobotAttachment : MonoBehaviour
{
    public Rigidbody2D rb;

    private float defaultTopSpeed = 5f;
    private float defaultAccFactor = 0.1f;
    private float defaultDragFactor = 0.2f;
    private float defaultMovementFuelPerSec = 5f;

    public virtual void RightArrow()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, defaultTopSpeed, defaultAccFactor);
        transform.parent.GetComponent<RobotController>().Fuel -= defaultMovementFuelPerSec * Time.fixedDeltaTime;
    }

    public virtual void LeftArrow()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, -1*defaultTopSpeed, defaultAccFactor); 
        transform.parent.GetComponent<RobotController>().Fuel -= defaultMovementFuelPerSec * Time.fixedDeltaTime;
    }

    public virtual void UpArrow()
    {

    }

    public virtual void Idle()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 0, defaultDragFactor);
    }
}
