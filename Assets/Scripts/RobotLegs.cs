using UnityEngine;

public class RobotLegs : RobotAttachment
{
    [SerializeField]
    private float jumpHeight = 1f;
    public override void UpArrow()
    {
        //This could be changed to world once we figure out the whole collision thing
        LayerMask mask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 0.5f, mask);
        if (hit)
        {
            rb.AddForce(new Vector2(0, Mathf.Sqrt(rb.gravityScale * jumpHeight)), ForceMode2D.Impulse);
        }
        
    }
}
