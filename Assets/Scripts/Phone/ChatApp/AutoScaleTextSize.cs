using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AutoScaleTextSize : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;

    [SerializeField] private RectTransform verticalRectTransform;
    [SerializeField] private RectTransform horizontalRectTransform;


    [SerializeField] private Image background;

    public float maxWidth;
    private void Start()
    {
        StartCoroutine(afterFrame());
    }

    IEnumerator afterFrame()
    {
        yield return new WaitForEndOfFrame();

        maxWidth = verticalRectTransform.rect.width;

        float preferredWidth = textComponent.preferredWidth+10;
        float preferredHeight = textComponent.preferredHeight+10;

        Debug.Log(preferredWidth);
        Debug.Log(preferredHeight);

        horizontalRectTransform.sizeDelta = new Vector2(Mathf.Clamp(preferredWidth + 20, 0, maxWidth), preferredHeight + 20);

        verticalRectTransform.sizeDelta = new Vector2(verticalRectTransform.sizeDelta.x, preferredHeight + 20);

        background.enabled = true;

    }
}
