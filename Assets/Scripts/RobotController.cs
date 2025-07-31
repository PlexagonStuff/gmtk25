using UnityEngine;

public class RobotController : MonoBehaviour
{

    public RobotAttachment attachment;
    public Rigidbody2D rb;

    public float fuel;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attachment.rb = rb;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            attachment.RightArrow();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            attachment.LeftArrow();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            attachment.UpArrow();
        }
        if (!Input.GetKey(KeyCode.UpArrow)) {
            attachment.Idle();
        }
    }
}
