using UnityEngine;

public class CrawlerEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public float groundCheckDistance = 1f;
    public LayerMask groundLayer;
    private bool movingRight = true;

    void Update()
    {

        // Move the block
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * (movingRight ? 1 : -1));

        // Cast a ray downward in front of the block
        Vector2 checkDirection = movingRight ? Vector2.right : Vector2.left;
        Vector2 origin = groundCheck.position;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider == null)
        {
            // No ground ahead, turn around
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
