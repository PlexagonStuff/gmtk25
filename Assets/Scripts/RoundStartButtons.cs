using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundStartButtons : MonoBehaviour
{
    public RobotController player;
    public RobotAttachment[] attachments;
    public void StartButton()
    {
        Scene gameScene = SceneManager.GetSceneByName("GameScene");
        RobotController playerObject = (RobotController)Instantiate(player, gameScene);
        GameObject realPlayerObject = playerObject.gameObject;
        Debug.Log(attachments[0]);
        Debug.Log(playerObject);
        Instantiate(attachments[0], new Vector2(-0.36f, -1.76f), Quaternion.identity, realPlayerObject.transform);
        realPlayerObject.transform.localScale = new Vector2(0.5f, 0.5f);
        realPlayerObject.transform.position = new Vector2(-1.04f, 3.16f);
        CameraSmoother[] cameras = FindObjectsByType<CameraSmoother>(FindObjectsSortMode.None);
        Debug.Log(cameras);
        foreach (CameraSmoother camera in cameras)
        {
            camera.player = realPlayerObject.transform;
        }
        
        SceneManager.UnloadSceneAsync("RoundStart");
    }
}
