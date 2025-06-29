using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSlotManager : MonoBehaviour
{
    public static WordSlotManager Instance;
    public List<Text> slots;
    private List<char> currentChars = new();
    public Button btn_Delete;

    public int score = 0;
    public AudioClip completeSound;

    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        btn_Delete.onClick.RemoveAllListeners();
        btn_Delete.onClick.AddListener(OnDeleteClick);
        UpdateSlots();
        UIManager.Instance.ShowScore(score);
    }

    public void AddCharacter(char c)
    {
        if (currentChars.Count >= slots.Count) return;

        currentChars.Add(c);
        UpdateSlots();

        if (SentenceDatabase.Instance.IsSentence(string.Concat(currentChars)))
        {
            score++;
            Invoke("PlayCompleteSound",1);
            GameManager.Instance.score=score;
            UIManager.Instance.ShowScore(score);
            UIManager.Instance.ChangeCurSentence(string.Concat(currentChars));
            currentChars.Clear();
            UpdateSlots();
        }
    }

    public void PlayCompleteSound()
    {
         MusicManager.Instance.PlayAduioClip(completeSound);
    }

    void UpdateSlots()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].text = i < currentChars.Count ? currentChars[i].ToString() : "";
    }

    public void OnDeleteClick()
    {
        if (currentChars.Count > 0)
        {
            currentChars.RemoveAt(currentChars.Count - 1);
        }
        UpdateSlots();
    }
}
