using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    { 
        SceneController.Instance.LoadScene(sceneName);
    }

    public void ChangeSceneAdditive(string sceneName)
    {
        SceneController.Instance.LoadSceneAdditive(sceneName);
    }

    public void UnloadScene(string sceneName)
    {
        SceneController.Instance.UnLoadScene(sceneName);
    }
}
