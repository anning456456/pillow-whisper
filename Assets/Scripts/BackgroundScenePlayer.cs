
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackgroundScenePlayer : MonoBehaviour
{
    public Button btn_Start;
    public BackgroundPanner backgroundPanner;
    public Image BGTitle;
    public Image girl1;
    public Image girl2;
    public Image Lan1;
    public Image Lan2;
    public Image Lan3;
    public Image Lan4;
    public Image Fan1;
    public Image MushRoom1;
    public Image MushRoom2;
    public Image Bear1;
    public Image Bear2;
    public Image Pillow;

    private Coroutine sceneCoroutine;
    private bool skipRequested = false;

    void Start()
    {
        girl1.gameObject.SetActive(false);
        girl2.gameObject.SetActive(false);
        Lan1.gameObject.SetActive(false);
        Lan2.gameObject.SetActive(false);
        Lan3.gameObject.SetActive(false);
        Lan4.gameObject.SetActive(false);
        Fan1.gameObject.SetActive(false);
        MushRoom1.gameObject.SetActive(false);
        MushRoom2.gameObject.SetActive(false);
        Bear1.gameObject.SetActive(false);
        Bear2.gameObject.SetActive(false);
        Pillow.gameObject.SetActive(false);

        btn_Start?.onClick.AddListener(() =>
        {
            btn_Start.gameObject.SetActive(false);
            BGTitle.gameObject.SetActive(false);
            sceneCoroutine = StartCoroutine(PlayScene());
        });
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            skipRequested = true;
        }
    }

    IEnumerator PlayScene()
    {
         yield return MoveAndWait(0f, 1f);   // 左边
        // 1. Girl1 出现
        yield return FadeIn(girl1);

girl1.GetComponent<PlayGirlAnim>().StartPlay();
        // 2. Fan1 出现
        yield return FadeIn(Fan1);

        // 3. 向右移动
        yield return MoveAndWait(0.25f, 1.5f);

        // 4. Girl1 消失
        yield return FadeOut(girl1,0.1f);

        // 5. Lan1 出现
        yield return FadeIn(Lan1,1f);

        // 6. Girl2 出现
        yield return FadeIn(girl2);

        girl2.GetComponent<PlayGirlAnim>().StartPlay();
        yield return new WaitForSeconds(2f);

        yield return FadeOut(Fan1,1f);

        // 7. Lan1 消失
        yield return FadeOut(Lan1,1f);

        // 8. Mushroom1 出现
        yield return FadeIn(MushRoom1);

        // 9. Lan2 出现
        yield return FadeIn(Lan2);

        // 10. 向右移动
        yield return MoveAndWait(0.75f, 1f);

  yield return FadeOut(MushRoom1);             // Mushroom1 消失
        yield return FadeIn(Bear1);                  // Bear1 出现
        yield return FadeIn(Lan3);                   // Lan3 出现
        yield return FadeOut(Bear1);                 // Bear1 消失

        yield return MoveAndWait(0.82f, 1f);          // 继续右移

        yield return FadeIn(Bear2);                  // Bear2 出现
        yield return FadeOut(Lan3,0.1f);                  // Lan3 消失
        yield return FadeIn(Lan4);
        yield return new WaitForSeconds(1f);                   // Lan4 出现
        yield return FadeOut(Lan4,0.1f);                  // Lan4 消失

        // Pillow 出现并播放闪烁动画
        yield return FadeIn(Pillow);
        StartCoroutine(BlinkLoop(Pillow));
        // 11. 动画结束
        Debug.Log("剧情动画播放完毕");
    }

    IEnumerator FadeIn(Image img, float duration = 1f)
    {
        img.gameObject.SetActive(true);
        img.color = new Color(1, 1, 1, 0);
        img.DOFade(1f, duration);
        return WaitOrSkip(duration);
    }

    IEnumerator FadeOut(Image img, float duration = 1f)
    {
        img.DOFade(0f, duration);
        yield return WaitOrSkip(duration);
        img.gameObject.SetActive(false);
    }

    IEnumerator WaitOrSkip(float duration)
    {
        float timer = 0f;
        skipRequested = false;
        while (timer < duration && !skipRequested)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        skipRequested = false;
    }

    IEnumerator MoveAndWait(float xNormalized, float waitTime)
    {
        skipRequested = false;
        backgroundPanner.MoveTo(xNormalized);

        while (backgroundPanner.IsMoving())
        {
            yield return null;
        }

        float timer = 0f;
        while (timer < waitTime && !skipRequested)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        skipRequested = false;
    }

 IEnumerator BlinkLoop(Image img)
    {
        while (true)
        {
            img.DOFade(0.3f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            img.DOFade(1f, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
