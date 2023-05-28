using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    public UnityEvent onPaused;
    public UnityEvent onContinued;

    public UnityEvent onGameStarted;
    public UnityEvent onGameLost;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
                Continue();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;

        UnlockCursor();
        onPaused.Invoke();
    }
    public void Continue()
    {
        isPaused = !isPaused;

        LockCursor();
        onContinued.Invoke();
    }
    public void StartTimeScale()
    {
        Time.timeScale = 1;
    }
    public void StopTimeScale()
    {
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        LockCursor();
        onGameStarted.Invoke();
    }
    public void LoseGame()
    {
        onGameLost.Invoke();
    }



    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
