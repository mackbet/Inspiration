using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Message : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private CharacterName character;
    [SerializeField] private bool owner;

    public void SetMyMessage(string text)
    {
        rect.pivot = new Vector2(1, 0);
        textField.text = text;

        gameObject.SetActive(true);
    }
    public void Set—ompanionMessage(string text)
    {
        rect.pivot = new Vector2(1, 0);
        textField.text = text;

        gameObject.SetActive(true);
    }
}
