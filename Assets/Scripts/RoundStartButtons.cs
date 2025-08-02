using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundStartButtons : MonoBehaviour
{
    public RobotController player;
    public RobotAttachment[] attachments;

    private InfoManager im;

    private GameObject startLoc;
    private void Start()
    {
        im = GameObject.Find("Manager").GetComponent<InfoManager>();
        startLoc = GameObject.Find("StartPos");
    }

    public void StartButton()
    {
        //make the basic player
        Scene gameScene = SceneManager.GetSceneByName("GameScene");
        RobotController playerObject = (RobotController)Instantiate(player, gameScene);
        GameObject realPlayerObject = playerObject.gameObject;
        //Debug.Log(attachments[0]);
        //Debug.Log(playerObject);

        //add the attachment based on selected button
        RobotAttachment attachment = im.selectedType.GetComponent<RobotAttachment>();
        Vector2 offset = im.selectedOffset;
        Instantiate(attachment, offset, Quaternion.identity, realPlayerObject.transform);

        //fix scale and stuff
        realPlayerObject.transform.position = startLoc.transform.position;
        realPlayerObject.transform.localScale = new Vector2(0.5f, 0.5f);
        CameraSmoother[] cameras = FindObjectsByType<CameraSmoother>(FindObjectsSortMode.None);
        Debug.Log(cameras);
        foreach (CameraSmoother camera in cameras)
        {
            camera.player = realPlayerObject.transform;
        }
        
        SceneManager.UnloadSceneAsync("RoundStart");
    }

    
}
