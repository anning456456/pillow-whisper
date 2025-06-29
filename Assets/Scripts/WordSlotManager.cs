using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSlotManager : MonoBehaviour
{
    public static WordSlotManager Instance;
    public List<Text> slots;
    private List<char> currentChars = new();
    public Button btn_Delete;

    void Awake()
    {
        Instance = this;
        btn_Delete.onClick.RemoveAllListeners();
        btn_Delete.onClick.AddListener(OnDeleteClick);
    }

    public void AddCharacter(char c)
    {
        if (currentChars.Count >= slots.Count) return;

        currentChars.Add(c);
        UpdateSlots();

        if (SentenceDatabase.Instance.IsSentence(string.Concat(currentChars)))
        {
            UIManager.Instance.ShowSentence(string.Concat(currentChars));
            currentChars.Clear();
            UpdateSlots();
        }
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
