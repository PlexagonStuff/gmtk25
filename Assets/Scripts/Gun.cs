using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool right = true;
    public float shotDelay = 0.1f;
    private float timer = 0f;
    public GameObject booletPrefab;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && timer <= 0f)
        {
            timer = shotDelay;
            GameObject temp =  Instantiate(booletPrefab, transform.position, transform.rotation);
            temp.GetComponent<Bullet>().right = right;
        }
    }
}

