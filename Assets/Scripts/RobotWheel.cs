using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class RobotWheel : RobotAttachment
{
    [SerializeField]
    private float jumpHeight = 1f;
    [SerializeField]
    private float selfBalanceFactor = 10;
    [SerializeField]
    private float jumpFuelCost = 5;

    Rigidbody2D rb;
    Animator anim;

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

        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));
    }

    public override void UpArrow()
    {
        //This could be changed to world once we figure out the whole collision thing
        LayerMask mask = LayerMask.GetMask("GroundLayer");
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -2f*transform.localScale.x), -transform.up, 0.1f, mask);
        if (hit)
        {
            rb.linearVelocityY = Mathf.Sqrt(2 * 9.81f*rb.gravityScale * jumpHeight);
            transform.parent.GetComponent<RobotController>().Fuel -= jumpFuelCost;

            anim.SetBool("jump", true);
            StartCoroutine(EndJump());
        }

    }

    private IEnumerator EndJump()
    {
        yield return new WaitForSeconds(0.05f);
        anim.SetBool("jump", false);
    }
}
