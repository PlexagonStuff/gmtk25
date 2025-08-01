using UnityEngine;

public class DefaultAttachment : RobotAttachment
{
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
    public override void LeftArrowUp()
    {
        base.LeftArrowUp();
        GetComponent<Animator>().speed = 0;
    }
    public override void RightArrowUp()
    {
        base.RightArrowUp();
        GetComponent<Animator>().speed = 0;
    }
}
