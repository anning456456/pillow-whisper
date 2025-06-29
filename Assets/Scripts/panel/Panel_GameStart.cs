using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel_GameStart : MonoBehaviour
{
    public Button btn_Start;
    public string targetScene;

    void Start()
    {
        btn_Start?.onClick.AddListener(() =>
        {
            SwitchScene(targetScene);
        });
    }

    /// <summary>
    /// 通用的切换场景方法
    /// </summary>
    /// <param name="sceneName">要切换到的场景名</param>
    public void SwitchScene(string sceneName)
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneName);
        //SceneManager.UnloadSceneAsync(currentScene);
    }
}
