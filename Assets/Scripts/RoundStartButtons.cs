using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundStartButtons : MonoBehaviour
{
    public RobotController player;
    public RobotAttachment[] attachments;
    public void StartButton()
    {
        Scene gameScene = SceneManager.GetSceneByName("SampleScene");
        RobotController playerObject = (RobotController)Instantiate(player, gameScene);
        GameObject realPlayerObject = playerObject.gameObject;
        Debug.Log(attachments[0]);
        Debug.Log(playerObject);
        Instantiate(attachments[0], realPlayerObject.transform, false);
        realPlayerObject.transform.localScale = new Vector2(0.2f, 0.2f);
        CameraSmoother[] cameras = FindObjectsByType<CameraSmoother>(FindObjectsSortMode.None);
        Debug.Log(cameras);
        foreach (CameraSmoother camera in cameras)
        {
            camera.player = realPlayerObject.transform;
        }
        
        SceneManager.UnloadSceneAsync("RoundStart");
    }
}
