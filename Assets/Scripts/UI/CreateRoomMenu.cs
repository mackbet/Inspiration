using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomName;

    public UnityEvent OnRoomCreated;
    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;


        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room " + PhotonNetwork.CurrentRoom.Name + " was created");
        OnRoomCreated.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode + " " + message);
    }
}
