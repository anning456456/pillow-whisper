using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BubbleManager : MonoBehaviour
{
    public HashSet<Bubble> bubbles = new();

    public BubbleSpawner spawner;
    public GameObject goPfb;

    public float grid = 100;

    public float spawnInterval = 1f;
    private float timer;
    private void Start()
    {
        spawner = new BubbleSpawner();
        spawner.InitPfb(goPfb);
    }

    void Update()
    {
        CheckBubblePos();


        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            AddBubble();
            timer = 0;
        }
    }

    public void CheckBubblePos()
    {
        var list = new List<Bubble>();
        var rect = (transform as RectTransform).rect;
        float yMax = rect.height * 0.5f - grid;
        foreach (var b in bubbles)
        {
            if (b == null)
            {
                list.Add(b);
                continue;
            }
            var rectTrans = b.GetComponent<RectTransform>();
            if (rectTrans.anchoredPosition.y > yMax)
            {
                list.Add(b);
            }
        }
        for (int i = 0; i < list.Count; i++)
        {

            bubbles.Remove(list[i]);
            if (list[i] != null)
            {
                list[i].state = Bubble.MoveState.OutArea;
            }
        }
    }

    public void AddBubble()
    {
        Vector2 pos = GenerationaSafePos();

        var bubble = spawner.SawnBubleAtPos(pos, transform);
        bubbles.Add(bubble);
    }

    public void BubbleWillDestroy(Bubble bubble)
    {
        bubbles.Remove(bubble);
    }

    public Vector2 GenerationaSafePos()
    {
        HashSet<int> hash = new();

        var rectTrans = transform as RectTransform;
        var rect = rectTrans.rect;

        foreach (var item in bubbles)
        {
            var effectArea = item.GetEffectPosArea(rect.min, grid);
            hash.AddRange(effectArea);
        }

        bool IsSafeArea(int x, int y)
        {
            return !hash.Contains(y * 1000 + x);
        }
        int width = Mathf.RoundToInt(rect.width / grid) - 2;
        int height = Mathf.RoundToInt(rect.height / grid) - 2;
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);
        int maxTryCount = 5;
        while (!IsSafeArea(x, y))
        {
            x = Random.Range(0, width);
            y = Random.Range(0, height);

            maxTryCount--;
            if (maxTryCount > 0)
            {
                continue;
            }
            Debug.LogError($"本次生成的资源位置异常");
            //找到最近的一个安全位置
            //这里需要一个兜底的方案
        }

        return new Vector2(x + 1, y + 1) * grid + rect.min;
        //return new Vector2(0, 0) * grid + rect.min;
    }



}
