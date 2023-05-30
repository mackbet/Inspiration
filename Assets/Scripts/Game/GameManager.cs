using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Linq;

public class GameManager : MonoBehaviourPun
{
    [SerializeField] Spawner _spawner;
    [SerializeField] ForestSpawner forestSpawner;
    [SerializeField] private Activator _monster;


    private void Start()
    {

        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        GameLoaded();

        forestSpawner.SpawnForest();
        _spawner.SpawnPlayer().GetComponent<Activator>().Activate();
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == GameEvents.PLAYER_LOADED_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            Player player = (Player)data[0];
            Debug.Log($"{player.NickName} loaded!");
        }
    }

    private void GameLoaded()
    {
        object[] data = new object[] { PhotonNetwork.LocalPlayer };
        PhotonNetwork.RaiseEvent(GameEvents.PLAYER_LOADED_EVENT, data, RaiseEventOptions.Default, SendOptions.SendReliable);
    }



    public void StartGame()
    {
        _spawner.SpawnPlayers(PhotonNetwork.CurrentRoom.Players.Values.ToArray());

    }



    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }
}
