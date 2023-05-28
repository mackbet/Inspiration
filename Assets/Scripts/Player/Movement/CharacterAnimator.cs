using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        _movement.onStateChanged += StateSwitched;
        _movement.onMoving += SetMoving;
    }

    private void StateSwitched(MovementState state)
    {
        animator.SetBool("isCrouching", state == MovementState.crouching);

    }
    private void SetMoving(Vector3 moveDirection)
    {
        animator.SetFloat("speed", moveDirection.magnitude);
    }

    public void SetDeath()
    {
        animator.SetFloat("speed", 0);
        animator.SetBool("isDead", true);
        Destroy(this);
    }
}
