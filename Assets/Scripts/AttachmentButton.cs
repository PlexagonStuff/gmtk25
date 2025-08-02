using UnityEngine;

public class AttachmentButton : MonoBehaviour
{
    public InfoManager.activeModule modType;
    private InfoManager im;
    public string attachmentName;
    public string attachmentDescription;
    private void Start()
    {
        im = GameObject.Find("Manager").GetComponent<InfoManager>();
    }

    public void SwichSelectedType()
    {
        im.SwichSelectedType(modType, attachmentName, attachmentDescription);
    }
}
