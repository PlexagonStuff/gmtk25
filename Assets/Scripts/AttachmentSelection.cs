using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AttachmentSelection : MonoBehaviour
{
    private List<Button> attachmentOptions = new List<Button>();
    private InfoManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Button button = transform.GetChild(i).gameObject.GetComponent<Button>();
            if (button != null)
            {
                attachmentOptions.Add(button);
                button.interactable = false;
            }
        }

        manager = GameObject.Find("Manager").GetComponent<InfoManager>();

        Debug.Log("here");
        attachmentOptions[0].interactable = true;
        if (manager.collectedWheel)
        {
            attachmentOptions[1].interactable = true;
        }
        if (manager.collectedhover)
        {
            attachmentOptions[2].interactable = true;
        }
    }
}
