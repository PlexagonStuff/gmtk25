using UnityEngine;
using UnityEngine.UIElements;

public class RobotWheel : RobotAttachment
{
    [SerializeField]
    private float jumpHeight = 1f;
    [SerializeField]
    private float selfBalanceFactor = 10;
    [SerializeField]
    private float jumpFuelCost = 5;

    private void FixedUpdate()
    {
        float scale = selfBalanceFactor * (Vector2.Dot(transform.up, Vector2.up) - 1) / -2;
        float sign = transform.up.x < 0 ? -1 : 1;
        rb.AddTorque(sign * scale);
    }

    public override void UpArrow()
    {
        Debug.Log("Up Pressed");
        //This could be changed to world once we figure out the whole collision thing
        LayerMask mask = LayerMask.GetMask("GroundLayer");
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f*transform.localScale.x), -transform.up, 0.1f, mask);
        if (hit)
        {
            rb.linearVelocityY = Mathf.Sqrt(2 * 9.81f*rb.gravityScale * jumpHeight);
            transform.parent.GetComponent<RobotController>().Fuel -= jumpFuelCost;
        }

    }
}
