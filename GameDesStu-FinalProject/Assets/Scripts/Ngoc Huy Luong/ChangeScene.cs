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
        Invoke("LoadDemoWorld", 0.2f);
    }
    public void LoadWorld1()
    {
        Invoke("LoadLoadWorld1", 0.2f);
    }
    public void LoadWorld2()
    {
        Invoke("LoadLoadWorld2", 0.2f);
    }
    public void LoadWorld3()
    {
        Invoke("LoadLoadWorld3", 0.2f);
    }
    public void LoadDemoWorld()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(2);
    }
    public void LoadLoadWorld1()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(3);
    }
    public void LoadLoadWorld2()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(4);
    }
    public void LoadLoadWorld3()
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
