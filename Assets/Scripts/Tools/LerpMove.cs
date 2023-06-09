using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class LerpMove : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 targetPos;
    [SerializeField] private Transform target;
    [SerializeField] private float duration = 1f;
    [SerializeField] private bool local;

    public UnityEvent onMoveStarted;
    public UnityEvent onMoveFinished;

    private Coroutine coroutine;
    [SerializeField]private bool flag;

    private void Awake()
    {
        if (!local)
        {
            startPos = transform.position;
            endPos = target.position;
        }
        else
        {
            startPos = transform.localPosition;
            endPos = target.localPosition;
        }
    }

    public void Move()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        if (!flag)
            targetPos = endPos;
        else
            targetPos = startPos;

        flag = !flag;
        coroutine = StartCoroutine(MoveCoroutine());
    }
    public void MoveToTarget()
    {
        if (flag)
            return;

        if (coroutine != null)
            StopCoroutine(coroutine);

        if (!flag)
            targetPos = endPos;

        flag = true;
        coroutine = StartCoroutine(MoveCoroutine());
    }
    public void MoveToStart()
    {
        if (!flag)
            return;

        if (coroutine != null)
            StopCoroutine(coroutine);

        if (flag)
            targetPos = startPos;

        flag = false;
        coroutine = StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        float timeElapsed = 0f;
        Vector3 currentPos = transform.position;

        if (local)
            currentPos = transform.localPosition;

        onMoveStarted.Invoke();

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;

            if(!local)
                transform.position = Vector3.Lerp(currentPos, targetPos, t);
            else
                transform.localPosition = Vector3.Lerp(currentPos, targetPos, t);


            timeElapsed += Time.deltaTime;
            yield return null;
        }

        onMoveFinished.Invoke();

        if(!local)
            transform.position = targetPos;
        else
            transform.localPosition = targetPos;
        coroutine = null;
    }
}
