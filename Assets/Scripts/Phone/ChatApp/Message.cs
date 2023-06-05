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
