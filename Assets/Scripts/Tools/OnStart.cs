using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{
    public UnityEvent onStart;

    public UnityEvent SpecialEvent;

    public UnityEvent DelayEvent;
    void Start()
    {
        onStart.Invoke();
    }

    public void InvokeSpecialEvent()
    {
        Debug.Log("InvokeSpecialEvent");
        SpecialEvent.Invoke();
    }

    public void CallEventAfterDelay(float duration)
    {
        StartCoroutine(delay(duration));
    }

    IEnumerator delay(float duration)
    {
        yield return new WaitForSeconds(duration);
        DelayEvent.Invoke();
    }
}
