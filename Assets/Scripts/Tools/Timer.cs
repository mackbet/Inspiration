using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent onTimer;
    public void StartTimer(float delay)
    {
        DOVirtual.DelayedCall(delay, () =>
        {
            onTimer.Invoke();
        });
    }
}
