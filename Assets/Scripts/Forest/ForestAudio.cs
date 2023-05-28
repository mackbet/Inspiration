using System.Collections;
using UnityEditor;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ForestAudio : MonoBehaviour
{
    public bool flag;
    public int i = 1;
    [SerializeField] AudioSource noiseAudioSource;
    [SerializeField] AudioSource windAudioSource;

    [SerializeField] SoundSource[] sounds;

    void Start()
    {
        noiseAudioSource.Play();
        windAudioSource.Play();

        foreach (SoundSource soundSource in sounds)
        {
            StartCoroutine(playSound(soundSource));
        }
    }

    IEnumerator playSound(SoundSource soundSource)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(soundSource.MinDelay, soundSource.MaxDelay));

            if (soundSource.BindedObjectTypes.Length > 0)
                soundSource.audioSource.transform.position = ForestSpawner.GetRandomPosition(soundSource.BindedObjectTypes[Random.Range(0, soundSource.BindedObjectTypes.Length)]);
            else
                soundSource.audioSource.transform.position = ForestSpawner.GetRandomPosition();

            soundSource.audioSource.clip = soundSource.clips[Random.Range(0, soundSource.clips.Length)];
            soundSource.audioSource.Play();
        }
    }
    [Serializable]
    private class SoundSource
    {
        public string Name;
        public AudioSource audioSource;
        public AudioClip[] clips;
        public float MinDelay;
        public float MaxDelay;

        public ObjectType[] BindedObjectTypes;
    }
}


