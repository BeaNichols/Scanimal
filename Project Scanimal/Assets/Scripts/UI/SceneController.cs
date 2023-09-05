using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    void Awake()
    {
        Application.targetFrameRate = 140;
        if (Instance != null)
        {
            Destroy(Instance);
            Instance = this;
        }
        else
        {
            Instance = this;
        }
    }
    public void OnClickScanScene()
    {
        SceneManager.LoadScene("BarcodeScanner",LoadSceneMode.Additive);
    }

    public void OnClickMainScene()
    {
        SceneManager.LoadScene("MainSceneTemp");
    }

    public void OnClickWalk()
    {
        SceneManager.LoadScene("WalkingScene");
    }

    public void OnClickCloseScanScene()
    {
        SceneManager.UnloadSceneAsync("BarcodeScanner");
    }

    public void OnClickCloseWalk()
    {
        SceneManager.UnloadSceneAsync("WalkingScene");
    }
}
