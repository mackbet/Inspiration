using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private float updateInterval = 0.5f;


    private float accum = 0f;
    private int frames = 0;
    private float timeLeft;

    bool isShown;
    void Start()
    {
        SettingsPanel.fpsPanelStateChanged += SetState;
        SetState(SettingsPanel.GetFPSPanelState());
    }

    private void Update()
    {
        if (isShown)
        {
            timeLeft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            frames++;

            if (timeLeft <= 0f)
            {
                float fps = accum / frames;
                textField.text = ((int)fps).ToString() + "f/s";

                timeLeft = updateInterval;
                accum = 0f;
                frames = 0;
            }
        }

    }

    private void SetState(bool state)
    {
        isShown = state;

        if (isShown)
        {
            textField.text = "";
            accum = 0f;
            frames = 0;
            timeLeft = updateInterval;
        }

        panel.SetActive(isShown);
    }
}
