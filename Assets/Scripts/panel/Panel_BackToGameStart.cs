using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Panel_BackToGameStart : MonoBehaviour
{
    public Button btn_Back;

    void Start()
    {
        btn_Back?.onClick.AddListener(() =>
        {
            var cenes = SceneManager.GetActiveScene();
            SceneManager.LoadScene("StartScene");
            SceneManager.UnloadSceneAsync(cenes);
        });
    }

}
