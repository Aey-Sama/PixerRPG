// using UnityEngine;

// public class SceneManagement : Singleton<SceneManagement>
// {
//     public string SceneTransitionName {get; private set;}

//     public void SetTransitionName(string sceneTransitionName){
//         this.SceneTransitionName = sceneTransitionName;
//     }
// }
using UnityEngine;
using System.Collections.Generic;

public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }

    // Track last positions per scene
    private Dictionary<string, Vector3> sceneLastPositions = new();

    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName;
    }

    public void SetLastPosition(string sceneName, Vector3 position)
    {
        if (sceneLastPositions.ContainsKey(sceneName))
            sceneLastPositions[sceneName] = position;
        else
            sceneLastPositions.Add(sceneName, position);
    }

    public Vector3? GetLastPosition(string sceneName)
    {
        if (sceneLastPositions.ContainsKey(sceneName))
            return sceneLastPositions[sceneName];
        return null;
    }
}
