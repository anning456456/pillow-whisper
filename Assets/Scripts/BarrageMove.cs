using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarrageMove : MonoBehaviour
{
    public Text SentenceText;
    public float floatSpeed = 50f;

    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private float textWidth;
    private float parentWidth;

    void Start()
    {
        //SetSentence();
        rectTransform = GetComponent<RectTransform>();
        if (transform.parent != null)
            parentRectTransform = transform.parent.GetComponent<RectTransform>();

        // 获取文本宽度
        if (SentenceText != null)
        {
            // 先强制刷新布局
            LayoutRebuilder.ForceRebuildLayoutImmediate(SentenceText.rectTransform);
            textWidth = SentenceText.preferredWidth;
        }
        else
        {
            textWidth = rectTransform.rect.width;
        }

        if (parentRectTransform != null)
        {
            parentWidth = parentRectTransform.rect.width;
        }
        else
        {
            parentWidth = Screen.width;
        }
        // 随机将y坐标加上100-300之间的值
        float randomYOffset = Random.Range(-50f, 100f);

        // 初始位置放在最左边外面
        rectTransform.anchoredPosition = new Vector2(-textWidth, rectTransform.anchoredPosition.y+randomYOffset);

         // 让弹幕速度在50-200之间随机
        floatSpeed = Random.Range(50f, 200f);
    }

    void Update()
    {
        if (rectTransform == null || parentRectTransform == null) return;

        // 向右移动
        rectTransform.anchoredPosition += new Vector2(floatSpeed * Time.deltaTime, 0);

        // 如果完全移出右侧，则从左侧重新进入
        if (rectTransform.anchoredPosition.x > parentWidth)
        {
            rectTransform.anchoredPosition = new Vector2(-textWidth, rectTransform.anchoredPosition.y);
        }
    }

    public void SetSentence()
    {
        SentenceText.text = SentenceDatabase.Instance.GetCurrentSentence();
    }

   
} 