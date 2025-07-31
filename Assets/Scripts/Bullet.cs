using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool right;
    public float bulletSpeed = 1f;
    public float liveTime = 10.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * bulletSpeed * Time.deltaTime * (right ? 1 : -1));
        liveTime -= Time.deltaTime;
        if (liveTime < 0f)
        {
            Destroy(gameObject);
        }
    }
}
