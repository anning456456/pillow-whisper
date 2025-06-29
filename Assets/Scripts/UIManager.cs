using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text timerText;
    public Text scoreText;
    public Text finalSentenceText;
    public GameObject endPanel;
    public GameObject successPanel;

    void Awake() => Instance = this;


    void Start()
    {
        // 初始设置句子的字体为灰色，句子背景为白色
        finalSentenceText.color = Color.gray;
        var bg = finalSentenceText.GetComponentInParent<Image>();
        if (bg != null)
        {
            bg.color = Color.white;
        }
    }

    

    public void UpdateTimer(float time)
    {
        timerText.text = Mathf.Max(0, Mathf.CeilToInt(time)).ToString();
    }

    public void ShowSentence(string sentence)
    {
        finalSentenceText.text = "完成句子：" + sentence;
    }

    public void ChangeCurSentence(string sentence)
    {
        finalSentenceText.text =  sentence;
    // 改变句子背景框和文字颜色
    var bg = finalSentenceText.GetComponentInParent<Image>();
    if (bg != null)
    {
        bg.color = new Color(0.6f, 0.9f, 0.6f, 1f); // 绿色调，表示完成
    }
    finalSentenceText.color = Color.blue; // 句子字变蓝色

    // 生成新句子，继续游戏
    SentenceDatabase.Instance.PickNewSentence();
    }


    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

     public void ShowSuccessPanel()
    {
        successPanel.SetActive(true);
    }

    public void ShowScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
