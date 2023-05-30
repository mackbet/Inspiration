using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviourPun
{
    [SerializeField] private Movement _movement;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private CameraRotator camera;
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(_movement);
            Destroy(camera.gameObject);

            _audioController.SetSpatialBlend(SpatialBlend.Sounds3D);
        }
    }

    public void DisableMovement()
    {
        if (_movement != null)
            _movement.enabled = false;
    }
    public void EnableMovement()
    {
        if (_movement != null)
            _movement.enabled = true;
    }

    public void DisableCameraRotator()
    {
        if (camera != null)
            camera.enabled = false;
    }
    public void EnableCameraRotator()
    {
        if (camera != null)
            camera.enabled = true;
    }
}
