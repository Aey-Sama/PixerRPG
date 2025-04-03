using UnityEngine;
using Unity.Cinemachine;
public class CameraControler : Singleton<CameraControler>
{
    private CinemachineCamera cinemachineCameral;

    public void SetPLayerCameraFollow(){
        cinemachineCameral = FindAnyObjectByType<CinemachineCamera>();
        cinemachineCameral.Follow = PlayerControler.Instance.transform;
    }
    
}
