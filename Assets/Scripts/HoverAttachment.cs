using UnityEngine;

public class HoverAttachment : RobotAttachment
{
    [SerializeField]
    private float selfBalanceFactor = 10;
    [SerializeField]
    private float jumpFuelCost = 0;

    Rigidbody2D rb;
    Animator anim;

    [SerializeField] private float targetHoverHeight = 1.5f;
    [SerializeField] private float hoverForce = 10f;
    [SerializeField] private float hoverDamping = 5f;
    [SerializeField] private float jumpDampening = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float targetJumpHoverHeight = 1.5f;
    public LayerMask groundLayer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float scale = selfBalanceFactor * (Vector2.Dot(transform.up, Vector2.up) - 1) / -2;
        float sign = transform.up.x < 0 ? -1 : 1;
        rb.AddTorque(sign * scale);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, targetHoverHeight, groundLayer);
        if (hit.collider != null)
        {
            float currentDistance = hit.distance;
            float heightDifference = targetHoverHeight - currentDistance;

            float upwardSpeed = rb.linearVelocity.y;

            // Spring force: pushes back toward the target height
            float springForce = heightDifference * hoverForce;

            // Damping force: reduces bobble overshoot
            float dampingForce = -upwardSpeed * hoverDamping;

            float totalForce = springForce + dampingForce;

            rb.AddForce(Vector2.up * totalForce, ForceMode2D.Force);
        }
    }

    public override void UpArrow()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, targetJumpHoverHeight, groundLayer);

        if (hit.collider != null)
        {
            float currentDistance = hit.distance;
            float heightDifference = targetJumpHoverHeight - currentDistance;

            float upwardSpeed = rb.linearVelocity.y;

            // Spring force: pushes back toward the target height
            float springForce = heightDifference * jumpForce;

            // Damping force: reduces bobble overshoot
            float dampingForce = -upwardSpeed * jumpDampening;

            float totalForce = springForce + dampingForce;

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
    }
}
