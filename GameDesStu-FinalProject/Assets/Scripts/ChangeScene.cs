using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneIndex;
    public int sceneIndex2;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void BackScene()
    {
        SceneManager.LoadSceneAsync(sceneIndex2);
    }
    public void Reset()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ExitGame()
    {
        //   Debug.Log("QuitGame");
        Application.Quit();
    }
}
