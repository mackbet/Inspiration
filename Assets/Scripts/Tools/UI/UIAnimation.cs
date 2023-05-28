using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
public class UIAnimation : MonoBehaviour
{

    [Header("Fade")]
    [SerializeField] private Image image;
    [SerializeField] private float delay;
    [Range(0f, 1f)]
    [SerializeField] private float opacity;

    [Header("Events")]
    public UnityEvent onPanelShown;
    public UnityEvent onPanelHiden;

    [Header("Move")]
    [SerializeField] private RectTransform _transform;
    [SerializeField] private float duration;
    [SerializeField] private RectTransform target;

    [Header("Events")]
    public UnityEvent onMovedToEnd;
    public UnityEvent onMovedToStart;

    private void Awake()
    {
        if(target!=null)
        {
            startPos= _transform.anchoredPosition;
            endPos= target.anchoredPosition;
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);

        Color startColor = image.color;
        Color targetColor = startColor;
        targetColor.a = opacity;

        image.DOColor(targetColor, delay).SetUpdate(true).OnComplete(() => { onPanelShown.Invoke(); });
    }
    public void Hide()
    {
        gameObject.SetActive(true);

        Color startColor = image.color;
        Color targetColor = startColor;
        targetColor.a = 0;

        image.DOColor(targetColor, delay).SetUpdate(true).OnComplete(() =>
        {
            onPanelHiden.Invoke();
            gameObject.SetActive(false);
        });
    }

    Vector2 startPos;
    Vector2 endPos;
    bool isMoved;
    public void Move()
    {
        Vector3 targetPosition;
        UnityEvent actualEvent;

        if (isMoved)
        {
            targetPosition = startPos;
            actualEvent = onMovedToStart;
        }
        else
        {
            targetPosition = endPos;
            actualEvent = onMovedToEnd;
        }
        isMoved = !isMoved;

        _transform.DOAnchorPos(targetPosition, duration).SetUpdate(true).OnComplete(() => { actualEvent.Invoke(); });
    }
}
