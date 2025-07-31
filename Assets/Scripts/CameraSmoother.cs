using UnityEngine;

public class CameraSmoother : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float maxDist = 3.0f;
    public Transform player;

    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        float step = moveSpeed * Time.deltaTime;
        Vector3 offset = player.position - transform.position;
        float distance = offset.magnitude;

        if (distance > 0f)
        {
            Vector3 direction = offset.normalized;
            float moveAmount = Mathf.Min(step, distance);

            // Clamp distance if it's over maxDist
            if (distance > maxDist)
            {
                // Move to exactly maxDist from player in the same direction
                transform.position = player.position - direction * maxDist;
            }
            else
            {
                transform.position += direction * moveAmount;
            }
        }
    }
}
