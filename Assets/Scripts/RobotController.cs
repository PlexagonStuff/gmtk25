using UnityEngine;

public class RobotController : MonoBehaviour
{

    private RobotAttachment attachment;
    private Rigidbody2D rb;

    public float fuel;
    void Start()
    {
        attachment = GetComponentInChildren<RobotAttachment>();

        rb = GetComponent<Rigidbody2D>();
        attachment.rb = rb;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        bool keyPressed = false;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            attachment.RightArrow();
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            attachment.LeftArrow();
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            attachment.UpArrow();
            keyPressed = true;
        }
        if (!keyPressed) {
            attachment.Idle();
        }
    }
}
