using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text timerText;
    public Text scoreText;
    public Text finalSentenceText;
    public GameObject endPanel;

    void Awake() => Instance = this;

    public void UpdateTimer(float time)
    {
        timerText.text = Mathf.Max(0, Mathf.CeilToInt(time)).ToString();
    }

    public void ShowSentence(string sentence)
    {
        finalSentenceText.text = "完成句子：" + sentence;
    }

    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

    public void ShowScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
