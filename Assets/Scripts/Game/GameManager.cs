using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Linq;
using UnityEngine.Events;

public class GameManager : MonoBehaviourPun
{
    [SerializeField] Spawner _spawner;
    private Monster _monster;
    [SerializeField] private Character _character;

    public UnityEvent onGameStarted;


    int playersCount;
    int loadedPlayersCount = 0;

    private void Start()
    {
        ForestSpawner.instance.SpawnForest();
        _character = _spawner.SpawnPlayer();
        _character.onDestroyed.AddListener(NextCamera);

        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        SendGameLoaded();
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == GameEvents.GAME_STARTED_EVENT)
        {
            StartGame();
        }
    }
    private void StartGame()
    {
        ForestSpawner.instance.audio.PlaySounds();
        _character.GetComponent<Activator>().Activate();

        if (PhotonNetwork.IsMasterClient)
        {
            _monster = _spawner.SpawnMonster();
            _monster.GetComponent<Activator>().Activate();
        }

        onGameStarted.Invoke();
    }

    public void SendGameLoaded()
    {
        base.photonView.RPC("RPC_GameLoaded", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }
    [PunRPC]
    private void RPC_GameLoaded(Player loadedPlayer)
    {
        Debug.Log($"Player {loadedPlayer.NickName} loaded!");

        playersCount = PhotonNetwork.CurrentRoom.Players.Count;
        loadedPlayersCount++;
        if (playersCount == loadedPlayersCount)
        {
            StartGame();
            PhotonNetwork.RaiseEvent(GameEvents.GAME_STARTED_EVENT, null, RaiseEventOptions.Default, SendOptions.SendReliable);
        }

    }


    private void NextCamera()
    {
        Debug.Log("NextCamera");
        Character[] leftCharacters = FindObjectsOfType<Character>();

        if (leftCharacters.Length > 0)
        {
            _character = leftCharacters[0];
            _character.onDestroyed.AddListener(NextCamera);
            leftCharacters[0].ActivateCamera();
        }
        else
        {
            Debug.Log("ALL ARE DEAD");
        }
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }


    #region CharacterMethods

    public void DisableCharacter()
    {
        if (!_character.photonView.IsMine)
            return;

        _character.DisableCameraRotator();
        _character.DisableMovement();
    }

    public void EnableCharacter()
    {
        if (!_character.photonView.IsMine)
            return;

        _character.EnableCameraRotator();
        _character.EnableMovement();
    }

    #endregion
}
