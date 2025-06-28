using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float spawnInterval = 1f;
    private float timer;
string[] bubbleColor=new string[]{"B1BDEF","F6DAAB","F7C9CD","BBD8F6","CCE8C8"};
string[] textColor=new string[]{"8A5FEC","D8A159","CA5568","2179F3","29A617"};


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
        Vector2 spawnPos = new Vector2(Random.Range(-400, 400),Random.Range(-600, 0));
        GameObject bubble = Instantiate(bubblePrefab, transform);
        bubble.GetComponent<RectTransform>().anchoredPosition = spawnPos;
        int index=Random.Range(0,bubbleColor.Length);
        bubble.GetComponent<Image>().color=HexToColor(bubbleColor[index]);
        bubble.GetComponent<Bubble>().SetCharacter(SentenceDatabase.Instance.GetRandomCharacter(),HexToColor(textColor[index]));
    }


      public static Color HexToColor(string hex)
    {
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        if (hex.Length != 6)
        {
            Debug.LogWarning("Hex string must be 6 characters long.");
            return Color.white;
        }

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, 255);
    }
} 