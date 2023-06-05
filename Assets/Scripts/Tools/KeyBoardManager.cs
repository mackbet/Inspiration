using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyBoardManager : MonoBehaviour
{
    [SerializeField] KeyBoardButton[] keyBoardButtons;

    private int unlockCount = 1;
    private void Update()
    {
        foreach (KeyBoardButton button in keyBoardButtons)
        {
            if (Input.GetKeyDown(button.keyCode))
            {
                button.onKeyDown.Invoke();
            }
        }
    }



    [System.Serializable]
    private class KeyBoardButton
    {
        [field: SerializeField] public KeyCode keyCode { get; private set; }
        public UnityEvent onKeyDown;
    }

    public void LockCursor()
    {
        unlockCount--;

        if (unlockCount > 0)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockCursor()
    {
        unlockCount++;

        if(unlockCount > 0)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
