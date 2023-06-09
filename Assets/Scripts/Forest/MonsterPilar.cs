using System;
using UnityEngine;
using UnityEngine.Events;

public class MonsterPilar : Interaction
{
    [SerializeField] private float delay;

    [SerializeField] private Action<Vector3> onActivated;

    public UnityEvent onPilarActivated;
    public override void Activate()
    {
        if (isActivated)
            return;

        SetOutline(false);
        isActivated = true;
        onPilarActivated.Invoke();

        MonsterPilarSpawner.activatedPilarCount++;
        if (MonsterPilarSpawner.activatedPilarCount == MonsterPilarSpawner.instance.pilarCount)
            MonsterPilarSpawner.instance.onPilarsActivated.Invoke();
    }
}
