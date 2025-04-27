using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEntrance : MonoBehaviour
{
   [SerializeField] private string transitionName;

    // private void Start()
    // {
    //     if (transitionName == SceneManagement.Instance.SceneTransitionName)
    //     {
    //         PlayerControler.Instance.transform.position = this.transform.position;
    //         CameraControler.Instance.SetPLayerCameraFollow();
    //         UIFade.Instance.FadeToClear();
            
    //         Debug.Log("PLayer ENter the Area ENtrance");
    //     }
    // }

//     private void Start()
// {
//     string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
//     Vector3? lastPos = SceneManagement.Instance.GetLastPosition(currentScene);

//     if (transitionName == SceneManagement.Instance.SceneTransitionName && lastPos == null)
//     {
//         // First time entering from a defined entrance
//         PlayerControler.Instance.transform.position = transform.position;
//     }
//     else if (lastPos != null)
//     {
//         // Returning to scene — use last remembered position
//         PlayerControler.Instance.transform.position = lastPos.Value;
//     }

//     CameraControler.Instance.SetPLayerCameraFollow();
//     UIFade.Instance.FadeToClear();

//     Debug.Log("PLayer ENter the Area ENtrance");
// }

private void Start()
{
    if (PlayerControler.Instance == null)
    {
        Debug.LogWarning("⚠️ PlayerControler.Instance not yet ready. Delaying AreaEntrance logic.");
        StartCoroutine(WaitForPlayerThenEnter());
        return;
    }

    HandlePlayerEntrance();
}

private IEnumerator WaitForPlayerThenEnter()
{
    yield return new WaitUntil(() => PlayerControler.Instance != null);
    yield return null; // Give 1 frame buffer

    HandlePlayerEntrance();
}

private void HandlePlayerEntrance()
{
    string currentScene = SceneManager.GetActiveScene().name;
    Vector3? lastPos = SceneManagement.Instance.GetLastPosition(currentScene);

    if (transitionName == SceneManagement.Instance.SceneTransitionName && lastPos == null)
    {
        PlayerControler.Instance.transform.position = transform.position;
    }
    else if (lastPos != null)
    {
        PlayerControler.Instance.transform.position = lastPos.Value;
    }

    CameraControler.Instance.SetPLayerCameraFollow();
    UIFade.Instance.FadeToClear();

    // Debug.Log("✅ [AFTER LOAD] Player exists? " + (PlayerControler.Instance != null));

}

   
}
