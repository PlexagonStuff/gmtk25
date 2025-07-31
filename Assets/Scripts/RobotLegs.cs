using UnityEngine;

public class RobotLegs : RobotAttachment
{
    [SerializeField]
    private float jumpHeight = 1f;
    public override void UpArrow()
    {
        rb.linearVelocityY = Mathf.Sqrt(2 * rb.gravityScale * jumpHeight);
    }
}
