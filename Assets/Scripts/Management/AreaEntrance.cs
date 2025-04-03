using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEntrance : MonoBehaviour
{
   [SerializeField] private string transitionName;

    private void Start()
    {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerControler.Instance.transform.position = this.transform.position;
            CameraControler.Instance.SetPLayerCameraFollow();
            UIFade.Instance.FadeToClear();
            
            Debug.Log("PLayer ENter the Area ENtrance");
        }
    }

   
}
