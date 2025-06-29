using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sentence : MonoBehaviour
{
    public Text SentenceText;
    public float floatSpeed = 50f;

    public GameObject sentenceMove;


    void Start()
    {
        SetSentence();
         lastSentence = SentenceDatabase.Instance.GetCurrentSentence();
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
            // 在父物体下生成一个新的Sentence对象，使用当前Sentence的颜色和句子，并让其像弹幕一样从左到右移动
            Transform parent = transform.parent;
            if (parent != null)
            {
                // 实例化一个新的Sentence对象
                GameObject newSentenceObj = Instantiate(sentenceMove, parent);
                // 获取新Sentence组件
                BarrageMove newSentence = newSentenceObj.GetComponent<BarrageMove>();
                if (newSentence != null)
                {
                    // 设置句子文本
                    newSentence.SentenceText.text = lastSentence;

                    //Debug.LogError(lastSentence);
                    // 设置颜色
                    newSentence.SentenceText.color = SentenceText.color;
                    // 设置背景色
                    var bg = SentenceText.GetComponentInParent<Image>();
                    var newBg = newSentence.SentenceText.GetComponentInParent<Image>();
                    if (bg != null && newBg != null)
                    {
                        newBg.color = bg.color;
                    }
                }
            }
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