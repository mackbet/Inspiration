using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsDetector : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private AliveScaner aliveScaner;
    [SerializeField] private float hearingRange;

    [SerializeField] private float noiseThreshold;
    private Dictionary<MonsterTracker, float> trackerNoiseList = new Dictionary<MonsterTracker, float>();
    private void Start()
    {
        MonsterTracker[] trackers = aliveScaner.trackers;

        foreach (MonsterTracker tracker in trackers)
        {
            trackerNoiseList.Add(tracker, 0);
            tracker.audioController.onSoundIsMade.AddListener((volume) => { DetectSound(volume, tracker); });
        }
    }


    private void DetectSound(float volume, MonsterTracker tracker)
    {
        if (tracker != null && Vector3.Distance(transform.position, tracker.transform.position) < hearingRange)
        {
            trackerNoiseList[tracker] += volume;

            if (trackerNoiseList[tracker] > noiseThreshold)
            {
                _monster.SetTarget(tracker);

                if (!corousines.ContainsKey(tracker))
                {
                    Coroutine coroutine = StartCoroutine(decreaseNoiseVolume(tracker));
                    corousines.Add(tracker, coroutine);
                }
            }
        }
    }

    private Dictionary<MonsterTracker, Coroutine> corousines=new Dictionary<MonsterTracker, Coroutine>();
    IEnumerator decreaseNoiseVolume(MonsterTracker tracker)
    {
        while (trackerNoiseList[tracker]>0)
        {
            yield return new WaitForSeconds(2);

            trackerNoiseList[tracker] -= 1;
        }

        _monster.SetTarget(null);
        trackerNoiseList[tracker] = 0;

        corousines.Remove(tracker);
    }
}
