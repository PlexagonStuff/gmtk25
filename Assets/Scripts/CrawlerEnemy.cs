using UnityEngine;
using System.Collections;

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
        Vector2 original = groundCheck.position;
        /*
        RaycastHit2D hit = Physics2D.Raycast(original,-groundCheck.up, groundCheckDistance, groundLayer);
        Debug.Log(original);
        Debug.Log(hit.point);
        Debug.DrawRay(original, hit.point, Color.red);
        if (hit.collider == null)
        {
            // No ground ahead, turn around
            Flip();
            return;
        }*/
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(2, 0), transform.right, groundCheckDistance, groundLayer.value);
        Debug.Log(hit.transform);
        Debug.Log(hit);
        Debug.Log(hit.point);
        Debug.DrawRay(transform.position, hit.point, Color.blue);
        if (hit)
        {
            Debug.Log("Does this work?");
            // No ground ahead, turn around
            Flip();
            return;
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        //if (collision.gameObject.layer == 6) {
            //Flip();
        //}        
    //}

    public IEnumerator Die()
    {
        Debug.Log("Got hit by corpse");
        GetComponent<Animator>().SetBool("Die", true);
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }


    private void Disappear()
    {
        Debug.Log("Event run?");
        Destroy(this.gameObject);
    }
}
