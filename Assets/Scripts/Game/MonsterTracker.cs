using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterTracker : MonoBehaviour
{
    [field: SerializeField] public Transform[] tags { get; private set; }

    public UnityEvent onTrackerCaptured;
    public void Dead()
    {
        onTrackerCaptured.Invoke();
        Destroy(this);
    }
}
