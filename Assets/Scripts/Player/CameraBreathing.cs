using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBreathing : MonoBehaviour
{
    [SerializeField] private float breathingSpeed = 1f;
    [SerializeField] private float breathingAmount = 0.1f;
    [SerializeField] private bool local;

    private Vector3 initialPosition;
    

    private void Start()
    {
        if(!local)
            initialPosition = transform.position;
        else
            initialPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        float breathingOffset = Mathf.Sin(Time.time * breathingSpeed) * breathingAmount;
        Vector3 newPosition = initialPosition + new Vector3(0f, breathingOffset, 0f);

        if (!local)
            transform.position = newPosition;
        else
            transform.localPosition = newPosition;
    }
}
