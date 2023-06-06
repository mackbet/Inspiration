using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource BreathAudioSources;

    [SerializeField] private AudioSource[] StepAudioSources;
    [SerializeField] private AudioClip[] stepSounds;

    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource crySound;

    public UnityEvent<float> onSoundIsMade;

    private void Start()
    {
        StartBreath();
    }

    private void StartBreath()
    {
        if (BreathAudioSources != null)
        {
            BreathAudioSources.Play();
        }
    }

    public void StepSound(float volume)
    {
        foreach (AudioSource StepAudioSource in StepAudioSources)
        {
            StepAudioSource.pitch = Random.Range(1.1f, 1.3f);
            StepAudioSource.clip = stepSounds[Random.Range(0, stepSounds.Length)];
            StepAudioSource.Play();

            if(onSoundIsMade!=null)
                onSoundIsMade.Invoke(volume);

            break;
        }
    }

    public void AttackSound()
    {
        attackSound.Play();
    }
    public void Cry()
    {
        crySound.Play();
    }


    public void SetSpatialBlend(SpatialBlend type)
    {
        foreach (AudioSource audioSource in StepAudioSources)
        {
            audioSource.spatialBlend = type == SpatialBlend.Sounds2D ? 0 : 1;
        }
    }
}

public enum SpatialBlend
{
    Sounds2D,
    Sounds3D,
}
