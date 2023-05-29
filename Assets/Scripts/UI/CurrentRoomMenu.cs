using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class CurrentRoomMenu : MonoBehaviourPunCallbacks
{

    [SerializeField] private PlayerListingsMenu playerListingsMenu;

    public UnityEvent onLeftRoom;

    public void OnButtonClick_Leave()
    {
        PhotonNetwork.LeaveRoom(true);
    }
    
    public void Show()
    {
        playerListingsMenu.Show();
    }

    public override void OnLeftRoom()
    {
        onLeftRoom.Invoke();
    }
}
