using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sentence : MonoBehaviour
{
    public Text SentenceText;
    public float floatSpeed = 50f;


    void Start()
    {
        SetSentence();
    }

    void Update()
    {
       
    
    }
    // 监听当前句子的变化，等待3秒后再更新句子内容
    private string lastSentence;
    private float sentenceChangeTime = -1f;
    private bool waitingToUpdate = false;

    void LateUpdate()
    {
        string current = SentenceDatabase.Instance.GetCurrentSentence();
        if (lastSentence != current)
        {
            // 检测到句子变化，记录时间，准备延迟更新
            sentenceChangeTime = Time.time;
            waitingToUpdate = true;
            lastSentence = current;
        }

        if (waitingToUpdate && Time.time - sentenceChangeTime >= 3f)
        {
            SetSentence();
            waitingToUpdate = false;
            InitSentence();
        }
    }

    public void InitSentence()
    {
         // 初始设置句子的字体为灰色，句子背景为白色
        SentenceText.color = Color.gray;
        var bg = SentenceText.GetComponentInParent<Image>();
        if (bg != null)
        {
            bg.color = Color.white;
        }
    }


    public void SetSentence()
    {
        SentenceText.text=SentenceDatabase.Instance.GetCurrentSentence();
    }

   
} 