using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bubble : MonoBehaviour, IPointerClickHandler
{
    public Image _image;
    public Text characterText;
    public float floatSpeed = 60f;
    private char character;

    public MoveState state = MoveState.MoveUp;


    void Awake()
    {
        _image = GetComponent<Image>();
        characterText = GetComponentInChildren<Text>();
    }


    void Update()
    {
        switch (state)
        {
            case MoveState.Spwan:
                break;
            case MoveState.MoveUp:
                transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
                //if (transform.position.y > Screen.height)
                //{
                   
                //}

                break;
            case MoveState.OutArea:
                Destroy(gameObject);
                //
                break;
            case MoveState.Clicked:
                break;

        }

    }


    public void SetData(Color color, char text, Color textColor)
    {
        _image.color = color;
        characterText.color = textColor;
        characterText.text = text.ToString();
    }

    public void SetCharacter(char c, Color _color)
    {
        character = c;
        characterText.text = c.ToString();
        characterText.color = _color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        WordSlotManager.Instance.AddCharacter(character);
        Destroy(gameObject); // 可加爆炸特效
    }

    public List<int> GetEffectPosArea(Vector2 posOffset, float grid)
    {
        var rectTrans = transform as RectTransform;
        var rect = rectTrans.rect;
        var list = new List<int>();

        rect.width = rect.width * 2 / grid;
        rect.height = rect.height * 2 / grid;
        rect.center -= posOffset;
        int xMin = Mathf.RoundToInt(rect.xMin);
        int xMax = Mathf.RoundToInt(rect.xMax);
        int yMin = Mathf.RoundToInt(rect.yMin);
        int yMax = Mathf.RoundToInt(rect.yMax);

        for (int y = yMin; y < yMax; y++)
        {
            for (int x = xMin; x < xMax; x++)
            {
                list.Add(y * 1000 + x);
            }
        }

        return list;
    }

    public enum MoveState
    {
        Spwan,
        MoveUp,
        OutArea,
        Clicked,
    }
}
