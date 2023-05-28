using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Transform model;
    [SerializeField] private float speed;

    private void Awake()
    {
        _movement.onMoving += FixRotation;
    }
    public void FixRotation(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            moveDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            model.rotation = Quaternion.RotateTowards(model.rotation, targetRotation, speed);
        }
    }
}
