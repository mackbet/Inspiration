using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AutoScaleTextSize : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private RectTransform rectTransform;

    public float maxWidth;
    private void OnEnable()
    {

        maxWidth = rectTransform.rect.width;


        float preferredWidth = textComponent.preferredWidth;
        float preferredHeight = textComponent.preferredHeight;


        rectTransform.sizeDelta = new Vector2(Mathf.Clamp(preferredWidth + 20, 0, maxWidth), preferredHeight + 20);
    }
}
