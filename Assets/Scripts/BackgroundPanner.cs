using UnityEngine;
using UnityEngine.UI;

public class BackgroundPanner : MonoBehaviour
{
    public RectTransform backgroundImage; // 你的长图 Image 的 RectTransform
    public float moveDuration = 2f; // 移动时间，秒
    private Vector2 targetPos;
    private Vector2 startPos;
    private float timer;
    private bool isMoving = false;

    public void MoveTo(float xNormalized)
    {
        // xNormalized 是 0~1，表示从左到右的百分比
        float imageWidth = backgroundImage.rect.width;
        float viewWidth = ((RectTransform)backgroundImage.parent).rect.width;

        float maxOffset = imageWidth - viewWidth;
        float targetX = -maxOffset * xNormalized;

        startPos = backgroundImage.anchoredPosition;
        targetPos = new Vector2(targetX, backgroundImage.anchoredPosition.y);
        timer = 0f;
        isMoving = true;
    }

    public bool IsMoving()
{
    return isMoving;
}


    void Update()
    {
        if (!isMoving) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / moveDuration);
        backgroundImage.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

        if (t >= 1f)
        {
            isMoving = false;
        }
    }
}
