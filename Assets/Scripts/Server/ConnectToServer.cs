using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public UnityEvent onConnectedToMaster;
    void Start()
    {
        PhotonNetwork.SendRate = 20;//20
        PhotonNetwork.SerializationRate = 5;//10
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName +  " connected to server");

        if(!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();

        onConnectedToMaster.Invoke();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined to lobby");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause.ToString());
    }
}
