using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSize : MonoBehaviour
{
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    void Start()
    {
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, size);
    }
}
