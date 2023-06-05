using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour
{
    private CharacterName myCharacter;
    private Dictionary<CharacterName, List<Message>> ChatHistory;
    [SerializeField] private Message MessagePrefab;
    [SerializeField] private Transform container;

    private CharacterName selectedChat = CharacterName.None;


    private void Start()
    {
        myCharacter = (CharacterName)PhotonNetwork.LocalPlayer.CustomProperties["CharacterName"];

        Player[] players = PhotonNetwork.CurrentRoom.Players.Values.ToArray();
    }

    public void OpenChat(ChatItem targetChat)
    {
        if (selectedChat!=CharacterName.None && !ChatHistory.ContainsKey(selectedChat))
        {
            List<Message> messages = ChatHistory[selectedChat];

            foreach (Message message in messages)
            {
                message.gameObject.SetActive(false);
            }
        }

        selectedChat = targetChat.Character;

        if (!ChatHistory.ContainsKey(selectedChat))
        {
            ChatHistory.Add(selectedChat, new List<Message>());
        }
        else
        {
            List<Message> messages = ChatHistory[selectedChat];

            foreach (Message message in messages)
            {
                message.gameObject.SetActive(false);
            }
        }
    }

    public void SendMessage(TMP_InputField inputField)
    {
        Message newMessage = Instantiate(MessagePrefab, container);

        newMessage.SetMyMessage(inputField.text);

    }

}
