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
        Cursor.lockState = CursorLockMode.None;

        if (PhotonNetwork.CurrentRoom != null)
            PhotonNetwork.LeaveRoom();

        PhotonNetwork.SendRate = 25;//20
        PhotonNetwork.SerializationRate = 15;//10
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
