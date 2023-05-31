using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviourPun
{
    [field:SerializeField] public CharacterName name { get; private set; }
    [SerializeField] private Movement _movement;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private CameraRotator camera;
    [SerializeField] private MonsterTracker _monsterTracker;

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(_movement);
            Destroy(camera.gameObject);

            _audioController.SetSpatialBlend(SpatialBlend.Sounds3D);

            _monsterTracker.onTrackerCaptured.AddListener(SendCharacterDead);
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


    public void SendCharacterDead()
    {
        Player player = photonView.Owner;

        base.photonView.RPC("RPC_PlayerDead", RpcTarget.All, player);

        Debug.Log($"{name} dead.");
    }
}

[System.Serializable]
public enum CharacterName
{
    None,
    John,
    Emma,
    Daniel,
    Isabellla
}
