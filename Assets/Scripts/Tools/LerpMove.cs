using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LerpMove : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float duration;
    private void Awake()
    {
        if (_transform != null)
        {
            startPos = _transform.localPosition;
            endPos = targetTransform.localPosition;
        }
    }
    Vector2 startPos;
    Vector2 endPos;
    bool isMoved;
    public void Move()
    {
        Vector3 targetPosition;

        if (isMoved)
        {
            targetPosition = startPos;
        }
        else
        {
            targetPosition = endPos;
        }
        isMoved = !isMoved;


    }
}
