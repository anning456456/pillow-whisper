using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // 游戏管理器：负责游戏计时与结束逻辑
    public float gameTime = 90f;
    private float timer;
   public int score;
   public PlayPillowAnim pillowanim;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = 0f;
        UIManager.Instance.UpdateTimer(gameTime);
        //UIManager.Instance.ShowScore(score);
    }

    void Update()
    {
        timer += Time.deltaTime;
        UIManager.Instance.UpdateTimer(gameTime - timer);

        if (timer >= gameTime)
            EndGame();
       // score = Mathf.FloorToInt(Time.time / 3);
        //UIManager.Instance.ShowScore(score);
    }

    void EndGame()
    {
        //Time.timeScale = 0;
        if(score>=2)
        {
        UIManager.Instance.ShowSuccessPanel();
        pillowanim.StartPlay();
        timer=0;
        }
        else
        {
            UIManager.Instance.ShowEndPanel();
            timer=0;
        }
        
       // UIManager.Instance.ShowScore(score);
    }
}
