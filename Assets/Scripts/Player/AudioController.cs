using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource BreathAudioSources;

    [SerializeField] private AudioSource[] StepAudioSources;
    [SerializeField] private AudioClip[] stepSounds;

    [SerializeField] private AudioSource attackSound;

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

    public void StepSound()
    {
        foreach (AudioSource StepAudioSource in StepAudioSources)
        {
           /* if (StepAudioSource.isPlaying)
                continue;*/

            StepAudioSource.pitch = Random.Range(1.1f, 1.3f);
            StepAudioSource.clip = stepSounds[Random.Range(0, stepSounds.Length)];
            StepAudioSource.Play();
            break;
        }
    }

    public void AttackSound()
    {
        attackSound.Play();
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
