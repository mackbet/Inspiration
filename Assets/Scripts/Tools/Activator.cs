using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    public bool isActive;
    public UnityEvent onActivated;
    public UnityEvent onDectivated;

    private void Start()
    {
        if (isActive)
            Activate();
    }

    bool isActivated;
    public void Activate()
    {
        if(!isActivated)
            onActivated.Invoke();
    }

    public void Dectivate()
    {
        if(isActivated)
            onDectivated.Invoke();
    }
}
