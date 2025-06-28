using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float spawnInterval = 1f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBubble();
            timer = 0;
        }
    }

    void SpawnBubble()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-300, 300), -Screen.height / 2);
        GameObject bubble = Instantiate(bubblePrefab, transform);
        bubble.GetComponent<RectTransform>().anchoredPosition = spawnPos;
        bubble.GetComponent<Bubble>().SetCharacter(SentenceDatabase.Instance.GetRandomCharacter());
    }
} 