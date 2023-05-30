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
    [SerializeField] private Activator _monster;
    private Character _character;

    public UnityEvent onGameStarted;


    int playersCount;
    int loadedPlayersCount = 0;

    private void Start()
    {
        ForestSpawner.instance.SpawnForest();
        _character = _spawner.SpawnPlayer();


        RPC_GameLoaded(PhotonNetwork.LocalPlayer);

        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
            SendGameLoaded();
        }
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == GameEvents.GAME_STARTED_EVENT)
        {
            StartGame();
            onGameStarted.Invoke();
        }
    }
    private void StartGame()
    {
        ForestSpawner.instance.audio.PlaySounds();
        _character.GetComponent<Activator>().Activate();
    }

    public void SendGameLoaded()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            base.photonView.RPC("RPC_GameLoaded", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
        }
    }
    [PunRPC]
    private void RPC_GameLoaded(Player loadedPlayer)
    {
        Debug.Log($"Player {loadedPlayer.NickName} loaded!");
        playersCount = PhotonNetwork.CurrentRoom.Players.Count;
        loadedPlayersCount++;
        if(playersCount== loadedPlayersCount)
            PhotonNetwork.RaiseEvent(GameEvents.GAME_STARTED_EVENT, null, RaiseEventOptions.Default, SendOptions.SendReliable);

    }


    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }
}
