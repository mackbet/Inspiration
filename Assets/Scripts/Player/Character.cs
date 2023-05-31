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

    private Player owner;
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(_movement);
            camera.enabled = false;
            camera.camera.gameObject.SetActive(false);

            _audioController.SetSpatialBlend(SpatialBlend.Sounds3D);
        }

        if(PhotonNetwork.IsMasterClient)
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
        base.photonView.RPC("RPC_PlayerDead", RpcTarget.Others, owner);

        Debug.Log($"{name} dead.");
    }

    [PunRPC]
    private void RPC_PlayerDead(Player player)
    {
        if (player == owner)
        {
            Destroy(_movement);
            _monsterTracker.Dead();
            //StartCoroutine(nextCamera());
        }
    }

    private IEnumerator nextCamera()
    {
        yield return new WaitForSeconds(3);

        CameraRotator[] cameras = FindObjectsOfType<CameraRotator>();

        if (cameras.Length > 0)
        {
            Destroy(camera.gameObject);
            cameras[0].camera.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("ALL ARE DEAD");
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
