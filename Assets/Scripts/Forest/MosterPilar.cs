using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MosterPilar : Interaction
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
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(delay);

        Monster monster =  Spawner.SpawnMonster(transform.position + Vector3.forward);

        monster.GetComponent<Activator>().Activate();
    }
}
