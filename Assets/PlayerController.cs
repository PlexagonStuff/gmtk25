using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int rotationSpeed;
    public float rotationAngle;

    public float fuel;

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
