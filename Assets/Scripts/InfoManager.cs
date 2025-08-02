using UnityEngine;
using TMPro;

public class InfoManager : MonoBehaviour
{
    public bool collectedWheel = false;
    public bool collectedhover = false;

    public enum activeModule
    {
        basic,
        wheel,
        hover
    }

    public activeModule am;

    public GameObject baseAttachment;
    public Vector2 baseOffset = new Vector2(-0.36f, -1.76f);
    public GameObject wheelAttachment;
    public Vector2 wheelOffset;
    public GameObject hoverAttachment;
    public Vector2 hoverOffset;

    public GameObject selectedType;
    public Vector2 selectedOffset;

    private TextMeshProUGUI attachNameTextBox;
    private TextMeshProUGUI attachDescTextBox;

    public void SwichSelectedType(activeModule modType, string attachmentName, string attachmentDescription)
    {
        attachNameTextBox = GameObject.Find("Attachment Name").GetComponent<TextMeshProUGUI>();
        attachDescTextBox = GameObject.Find("AttachmentBody").GetComponent<TextMeshProUGUI>();
        switch (modType)
        {
            case (activeModule.basic):
                selectedType = baseAttachment;
                selectedOffset = baseOffset;
                break;
            case (activeModule.wheel):
                selectedType = wheelAttachment;
                selectedOffset = wheelOffset;
                break;
            case (activeModule.hover):
                selectedType = hoverAttachment;
                selectedOffset = hoverOffset;
                break;
        }
        attachNameTextBox.text = attachmentName;
        attachDescTextBox.text = attachmentDescription;
    }
}
