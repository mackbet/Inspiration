using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviourPun
{

    [field:SerializeField] public CharacterName name { get; private set; }
    [SerializeField] private Movement _movement;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private CharacterAnimator _characterAnimator;
    [SerializeField] private CameraRotator cameraRotator;
    [SerializeField] private MonsterTracker _monsterTracker;

    private Player owner;

    public UnityEvent onDestroyed;
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(_movement);
            cameraRotator.enabled = false;
            cameraRotator.camera.gameObject.SetActive(false);

            _audioController.SetSpatialBlend(SpatialBlend.Sounds3D);

        }

        if (PhotonNetwork.IsMasterClient)
            _monsterTracker.onTrackerCaptured.AddListener(SendCharacterDead);

        owner = photonView.Owner;
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
        if (cameraRotator != null)
            cameraRotator.enabled = false;
    }
    public void EnableCameraRotator()
    {
        if (cameraRotator != null)
            cameraRotator.enabled = true;
    }
    private void SendCharacterDead()
    {
        base.photonView.RPC("RPC_PlayerDead", RpcTarget.All, owner);

        Debug.Log($"{name} dead.");
    }

    [PunRPC]
    private void RPC_PlayerDead(Player player)
    {
        Debug.Log(player.CustomProperties["Nickname"] + " dead!");
        if (player == owner)
        {
            Dead();
            StartCoroutine(DestroyCharacter());
        }
    }

    private void Dead()
    {
        Destroy(_movement);
        _characterAnimator.SetDeath();
    }

    private IEnumerator DestroyCharacter()
    {
        yield return new WaitForSeconds(5);

        Destroy(_movement.gameObject);
        onDestroyed.Invoke();
        Destroy(this);
    }

    public void ActivateCamera()
    {
        if (!photonView.IsMine)
        {
            cameraRotator.camera.gameObject.SetActive(true);
        }
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
