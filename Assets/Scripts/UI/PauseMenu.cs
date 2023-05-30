using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool isPaused;
    [field:SerializeField] public bool isAnimating { get; set; }

    public UnityEvent onPaused;
    public UnityEvent onContinued;

    public void PlayPause()
    {
        if (isAnimating)
            return;

        isPaused = !isPaused;

        if(isPaused)
            onPaused.Invoke();
        else
            onContinued.Invoke();
    }
}
