using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _outline;
    [SerializeField] private Image _image;


    [SerializeField] private Color disabledColor;

    private Color _defaultColor;

    public UnityEvent onClick;

    private void Awake()
    {
        _defaultColor = _image.color;
        _button.onClick.AddListener(() => { onClick.Invoke(); });
    }
    public void EnableOutline()
    {
        _outline.SetActive(true);
    }
    public void DisableOutline()
    {
        _outline.SetActive(false);
    }
    public void EnableButton()
    {
        _button.interactable = true;
        _image.color = _defaultColor;
    }
    public void DisableButton()
    {
        _button.interactable = false;
        _image.color = disabledColor;
    }
}
