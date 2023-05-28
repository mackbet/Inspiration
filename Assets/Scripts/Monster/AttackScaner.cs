using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScaner : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MonsterTracker target))
        {
            _monster.Attack(target);
        }
    }
}
