using UnityEngine;

public class DefaultAttachment : RobotAttachment
{
    public void Start()
    {
        GetComponent<Animator>().speed = 0;
    }

    public override void LeftArrow()
    {
        base.LeftArrow();
        GetComponent<Animator>().speed = 1;
    }

    public override void RightArrow()
    {
        base.RightArrow();
        GetComponent<Animator>().speed = 1;
    }
    public override void NoLeftRight()
    {
        base.NoLeftRight();
        GetComponent<Animator>().speed = 0;
    }
}
