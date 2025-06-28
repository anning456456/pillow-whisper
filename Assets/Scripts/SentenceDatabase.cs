using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SentenceDatabase : MonoBehaviour
{
    public static SentenceDatabase Instance;

    private List<string> sentences = new();       // 全部句子
    private string currentSentence;               // 当前目标句子
    private List<char> shuffledChars = new();     // 当前句子的打乱字符列表

    void Awake()
    {
        Instance = this;

        // 载入词库
        TextAsset textAsset = Resources.Load<TextAsset>("Sentences");
        sentences = textAsset.text.Split('\n').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList();

        PickNewSentence();
    }

    public void PickNewSentence()
    {
        currentSentence = sentences[Random.Range(0, sentences.Count)];
        shuffledChars = currentSentence.ToList();
        Shuffle(shuffledChars);
    }

    public char GetRandomCharacter()
    {
        if (shuffledChars.Count == 0) PickNewSentence();
        return shuffledChars[Random.Range(0, shuffledChars.Count)];
    }

    public bool IsSentence(string input)
    {
        return input == currentSentence;
    }

    public string GetCurrentSentence() => currentSentence;

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}