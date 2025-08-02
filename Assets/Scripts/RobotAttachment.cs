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


    private Vector2 checkSlope(Vector2 direction)
    {
        LayerMask mask = LayerMask.GetMask("GroundLayer");
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f * transform.localScale.x), -transform.up, 0.5f, mask);
        Debug.Log(hit.normal);
        Debug.Log(transform.up);
        if (hit.normal != new Vector2(0, 1))

        {
            Debug.Log("Current Direction");
            Debug.Log((Vector3)direction);
            Debug.Log("Hit Normal");
            Debug.Log((Vector3)hit.normal);
            return (Vector2)Vector3.ProjectOnPlane((Vector3)direction, (Vector3)hit.normal);
        }
        else
        {
            return direction;
        }
    }
    public virtual void RightArrow()
    {
        Vector2 direction = checkSlope(new Vector2(1, 0));
        Mathf.Lerp(rb.linearVelocityX, topSpeed, accFactor);
        Debug.Log(direction);
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX,topSpeed, accFactor);
        transform.parent.GetComponent<RobotController>().Fuel -= movementFuelPerSec * Time.fixedDeltaTime;
    }


    public virtual void LeftArrow()
    {
        Vector2 direction = checkSlope(new Vector2(-1, 0));
        Mathf.Lerp(rb.linearVelocityX, topSpeed, accFactor);
        Debug.Log(direction);
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, -1 * topSpeed, accFactor);
        transform.parent.GetComponent<RobotController>().Fuel -= movementFuelPerSec * Time.fixedDeltaTime;
    }


    public virtual void UpArrow()
    {

    }


    public virtual void Idle()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 0, dragFactor);
    }

    public virtual void NoLeftRight()
    {

    }
}
