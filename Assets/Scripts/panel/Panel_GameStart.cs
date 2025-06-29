using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Panel_GameStart : MonoBehaviour
{
    public Button btn_Start;
    void Start()
    {
        btn_Start?.onClick.AddListener(() =>
        {
            var cenes = SceneManager.GetActiveScene();
            SceneManager.LoadScene("GameScene");
            //SceneManager.UnloadSceneAsync(cenes);
        });
    }

}
