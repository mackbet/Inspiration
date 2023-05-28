using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioListener : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string groupName;
    private void Start()
    {
        SettingsPanel.onAudioVolumeChanged += SetAudioVolume;
        SetAudioVolume(SettingsPanel.GetAudioVolume());

        float fff;
        audioMixer.GetFloat(groupName + "Volume", out fff);
    }
    private void SetAudioVolume(float newVolume)
    {
        audioMixer.SetFloat(groupName+ "Volume", newVolume);
    }
}
