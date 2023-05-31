using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] private Vector3 axes;
    void Start()
    {
        transform.localEulerAngles = new Vector3(Mathf.Clamp01(axes.x) * RandomHelper.GetRandomFloat(0, 360), Mathf.Clamp01(axes.y) * RandomHelper.GetRandomFloat(0, 360), Mathf.Clamp01(axes.z) * RandomHelper.GetRandomFloat(0, 360));
    }
}
