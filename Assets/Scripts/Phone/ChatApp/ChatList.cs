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
    [SerializeField] private ChatManager chatManager;

    private void Start()
    {
        Player[] players = PhotonNetwork.CurrentRoom.Players.Values.ToArray();

        foreach (Player player in players)
        {
            if (player != PhotonNetwork.LocalPlayer)
            {
                ChatItem newChatItem = Instantiate(chatItem, container);

                newChatItem.SetPlayer(player);

                newChatItem.onClick.AddListener(() => chatManager.OpenChat(newChatItem));
            }
        }
    }
}
