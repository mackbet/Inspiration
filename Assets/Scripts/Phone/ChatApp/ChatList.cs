using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChatList : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private ChatItem chatItem;

    private void Start()
    {
        Player[] players = PhotonNetwork.CurrentRoom.Players.Values.ToArray();

        foreach (Player player in players)
        {
        }
    }
}
