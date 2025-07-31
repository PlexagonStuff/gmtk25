using UnityEngine;

public abstract class RobotAttachment : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void RightArrow();

    public abstract void LeftArrow();

    public abstract void UpArrow();

    public abstract void Idle();
}
