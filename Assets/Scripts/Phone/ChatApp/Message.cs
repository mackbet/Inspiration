using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Message : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private TextMeshProUGUI textField;

    public void SetMyMessage(string text)
    {
        rect.pivot = new Vector2(1, 0);

        Vector2 newPosition = rect.anchoredPosition;
        newPosition.x = 0;
        rect.anchoredPosition = newPosition;

        textField.text = text;

        gameObject.SetActive(true);
    }
    public void Set—ompanionMessage(string text)
    {
        rect.pivot = new Vector2(0, 0);
        textField.text = text;

        gameObject.SetActive(true);
    }
}
