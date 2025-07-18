using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGirlAnim : MonoBehaviour
{
    // Start is called before the first frame update
    // 轮播图片相关变量
    public List<Sprite> sprites; // 需要轮播的图片列表
    public UnityEngine.UI.Image targetImage; // 显示图片的Image组件
    public float switchInterval = 2f; // 每张图片显示的时间（秒）
    private int currentIndex = 0;
    private float timer = 0f;
    private bool isPlaying = true;

    void Start()
    {
            currentIndex = 0;
            timer = 0f;
            isPlaying = false;
    }

    public  void StartPlay()
    {
        //Debug.LogError("1111111");
         currentIndex = 0;
            targetImage.sprite = sprites[currentIndex];
            timer = 0f;
      isPlaying = true;
    }



    void Update()
    {
        if (!isPlaying) return;

        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            timer = 0f;
            currentIndex++;
            if (currentIndex < sprites.Count)
            {
                targetImage.sprite = sprites[currentIndex];
            }
            else
            {
                // 轮播完毕，停止
                isPlaying = false;
            }
        }
    }
}
