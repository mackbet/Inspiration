using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        _monster.onStateChanged += StateSwitched;
        _monster.onMoving += SetSpeed;
    }

    private void StateSwitched(MonsterState state)
    {
        Debug.Log(state);
        switch (state)
        {
            case MonsterState.idle:
                animator.SetTrigger("Idle");
                break;
            case MonsterState.searching:
                animator.SetTrigger("Search");
                break;
            case MonsterState.attacking:
                animator.SetTrigger("Attack");
                break;
        }

    }
    private void SetSpeed(float speed)
    {
       animator.SetFloat("speed", speed);
    }
}
