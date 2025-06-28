using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bubble : MonoBehaviour, IPointerClickHandler
{
    public Text characterText;
    public float floatSpeed = 50f;
    private char character;

    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        if (transform.position.y > Screen.height)
            Destroy(gameObject);
    }

    public void SetCharacter(char c)
    {
        character = c;
        characterText.text = c.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        WordSlotManager.Instance.AddCharacter(character);
        Destroy(gameObject); // 可加爆炸特效
    }
} 