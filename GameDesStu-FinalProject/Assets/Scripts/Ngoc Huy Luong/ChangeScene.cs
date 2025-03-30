using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void DemoWorld()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(2);
    }
    public void LoadWorld1()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(3);
    }
    public void LoadWorld2()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(4);
    }
    public void LoadWorld3()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(5);
    }
    public void BackScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void ExitGame()
    {
        //   Debug.Log("QuitGame");
        Application.Quit();
    }
}
