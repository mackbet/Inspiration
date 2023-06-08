using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAudio : MonoBehaviour
{
    [SerializeField] private ChatManager chatManager;
    [SerializeField] private AudioSource notificationAudio;

    public void Start()
    {
        chatManager.onGotMessage.AddListener(PlayNotificationSound);
    }
    private void PlayNotificationSound()
    {
        notificationAudio.Play();
    }
}
