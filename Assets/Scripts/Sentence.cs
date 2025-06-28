using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sentence : MonoBehaviour
{
    public Text SentenceText;
    public float floatSpeed = 50f;


    void Start()
    {
        SetSentence();
    }

    void Update()
    {
       
    
    }

    public void SetSentence()
    {
        SentenceText.text=SentenceDatabase.Instance.GetCurrentSentence();
    }

   
} 