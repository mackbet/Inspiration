using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public static Action<float> onSensitivityChanged;
    [SerializeField] private Slider sensitivitySlider;


    public static Action<float> onAudioVolumeChanged;
    [SerializeField] private Slider audioVolumeSlider;


    public static Action<bool> fpsPanelStateChanged;
    [SerializeField] private Toggle fpsPanelToggle;

    private void Start()
    {
        InitSensitivity();
        InitAudioVolume();
        InitFPSPanelState();
    }

    #region Sensitivity
    private void InitSensitivity()
    {
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);

        float savedSensetivity = PlayerPrefs.GetFloat("sensitivity");

        if (savedSensetivity == 0)
            sensitivitySlider.value = 1;
        else
            sensitivitySlider.value = savedSensetivity;
    }
    private void ChangeSensitivity(float value)
    {
        PlayerPrefs.SetFloat("sensitivity", value);

        if(onSensitivityChanged!=null)
            onSensitivityChanged.Invoke(value * 50);
    }

    public static float GetSensitivity()
    {
        float savedSensetivity = PlayerPrefs.GetFloat("sensitivity");
        return 50 * savedSensetivity;
    }

    #endregion

    #region AudioVolume
    private void InitAudioVolume()
    {
        audioVolumeSlider.onValueChanged.AddListener(ChangeAudioVolume);

        float savedAudioVolume = PlayerPrefs.GetFloat("audioVolume");

        if (savedAudioVolume == 0)
            audioVolumeSlider.value = 1;
        else
            audioVolumeSlider.value = savedAudioVolume;
    }
    private void ChangeAudioVolume(float value)
    {
        PlayerPrefs.SetFloat("audioVolume", value);

        if (onAudioVolumeChanged != null)
            onAudioVolumeChanged.Invoke(value);
    }

    public static float GetAudioVolume()
    {
        float savedAudioVolume = PlayerPrefs.GetFloat("audioVolume");
        return savedAudioVolume;
    }

    #endregion

    #region FPSPanel

    private void InitFPSPanelState()
    {
        fpsPanelToggle.onValueChanged.AddListener(ChangeFPSPanelState);

        float savedFPSPanelState = PlayerPrefs.GetInt("fpsPanelState");

        if (savedFPSPanelState == 0)
            fpsPanelToggle.isOn = false;
        else
            fpsPanelToggle.isOn = true;
    }
    private void ChangeFPSPanelState(bool state)
    {
        PlayerPrefs.SetInt("fpsPanelState", state ? 1 : 0);

        if (fpsPanelStateChanged != null)
            fpsPanelStateChanged.Invoke(state);
    }

    public static bool GetFPSPanelState()
    {
        float savedFPSPanelState = PlayerPrefs.GetInt("fpsPanelState");
        return savedFPSPanelState > 0;
    }

    #endregion
}
