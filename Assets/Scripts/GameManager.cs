using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 游戏管理器：负责游戏计时与结束逻辑
    public float gameTime = 90f;
    private float timer;

    void Start()
    {
        timer = 0f;
        UIManager.Instance.UpdateTimer(gameTime);
    }

    void Update()
    {
        timer += Time.deltaTime;
        UIManager.Instance.UpdateTimer(gameTime - timer);

        if (timer >= gameTime)
            EndGame();
    }

    void EndGame()
    {
        Time.timeScale = 0;
        UIManager.Instance.ShowEndPanel();
    }
}
