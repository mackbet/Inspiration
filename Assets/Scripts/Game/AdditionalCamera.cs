using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float distance;

    [SerializeField] private Vector2Int symmetric;

    private void Start()
    {
        Debug.Log(Vector3.Distance(_camera.position,mainCamera.position));
    }

    private void Update()
    {
        _camera.rotation = mainCamera.rotation;

        Vector3 newPosition = mainCamera.position;

        newPosition.x += distance * symmetric.x;
        newPosition.z += distance *symmetric.y;

        _camera.position = newPosition;
    }
}
