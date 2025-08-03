using UnityEngine;
using System.Collections;

public class FloatingEnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 2f;
    private bool movingRight = true;

    void Update()
    {
        // Move the block
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime * (movingRight ? 1 : -1));
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Flip();
    }

    public IEnumerator Die()
    {
        GetComponent<Animator>().SetBool("Die", true);
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
