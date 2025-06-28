using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class BubbleSpawner
{
    public GameObject bubblePrefab;
    //public float spawnInterval = 1f;
    //private float timer;
    string[] bubbleColor = new string[] { "B1BDEF", "F6DAAB", "F7C9CD", "BBD8F6", "CCE8C8" };
    string[] textColor = new string[] { "8A5FEC", "D8A159", "CA5568", "2179F3", "29A617" };

    public void InitPfb(GameObject go)
    {
        bubblePrefab = go;
    }
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (timer >= spawnInterval)
    //    {
    //        SpawnBubble();
    //        timer = 0;
    //    }
    //}

    void SpawnBubble(Transform parent = null)
    {
        Vector2 spawnPos = new Vector2(Random.Range(-400, 400), Random.Range(-600, 0));
        GameObject bubble = GameObject.Instantiate(bubblePrefab, parent);
        bubble.GetComponent<RectTransform>().anchoredPosition = spawnPos;
        int index = Random.Range(0, bubbleColor.Length);
        bubble.GetComponent<Image>().color = HexToColor(bubbleColor[index]);
        bubble.GetComponent<Bubble>().SetCharacter(SentenceDatabase.Instance.GetRandomCharacter(), HexToColor(textColor[index]));
    }

    public Bubble SawnBubleAtPos(Vector2 pos, Transform parent = null)
    {
        int index = Random.Range(0, bubbleColor.Length);
        GameObject go = GameObject.Instantiate(bubblePrefab, parent);
        go.SetActive(false);
        go.GetComponent<RectTransform>().anchoredPosition = pos;
        Bubble bubble = go.GetComponent<Bubble>();
        var color1 = HexToColor(bubbleColor[index]);
        var color2 = HexToColor(textColor[index]);
        var _char = SentenceDatabase.Instance.GetRandomCharacter();
        bubble.SetData(color1, _char, color2);
        bubble.gameObject.SetActive(true);
        return bubble;
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
