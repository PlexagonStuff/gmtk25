using UnityEngine;

public class RobotAttachment : MonoBehaviour
{
    public Rigidbody2D rb;

    private float defaultTopSpeed = 5;
    private float defaultAccFactor = 0.1f;
    private float defaultDragFactor = 0.2f;

    public virtual void RightArrow()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, defaultTopSpeed, defaultAccFactor);
        Debug.Log(rb.linearVelocityX);
    }

    public virtual void LeftArrow()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, -1*defaultTopSpeed, defaultAccFactor); 
    }

    public virtual void UpArrow()
    {

    }

    public virtual void Idle()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 0, defaultDragFactor);
    }
}
