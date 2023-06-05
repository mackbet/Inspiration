using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviourPunCallbacks
{
    private CharacterName myCharacter;
    [SerializeField] private Transform container;

    private Dictionary<CharacterName, List<Message>> ChatHistory = new Dictionary<CharacterName, List<Message>>();
    [SerializeField] private Message MessagePrefab;
    [SerializeField] private GameObject ChatPage;


    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;

    private CharacterName selectedChat = CharacterName.None;
    private Player selectedPlayer = null;


    private void Start()
    {
        myCharacter = (CharacterName)PhotonNetwork.LocalPlayer.CustomProperties["CharacterName"];

        Player[] players = PhotonNetwork.CurrentRoom.Players.Values.ToArray();
    }

    public void OpenChat(ChatItem targetChat)
    {
        ChatPage.SetActive(true);

        if (selectedChat!=CharacterName.None && !ChatHistory.ContainsKey(selectedChat))
        {
            List<Message> messages = ChatHistory[selectedChat];

            foreach (Message message in messages)
            {
                message.gameObject.SetActive(false);
            }
        }

        selectedChat = targetChat.Character;
        selectedPlayer = targetChat.Player;

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

        ChatHistory[selectedChat].Add(newMessage);

        base.photonView.RPC("RPC_ChatMessage", selectedPlayer, inputField.text, myCharacter);


        StartCoroutine(updateMessageList());
    }
    private void RPC_ChatMessage(string text, CharacterName owner)
    {
        Message newMessage = Instantiate(MessagePrefab, container);

        newMessage.SetMyMessage(text);

        ChatHistory[owner].Add(newMessage);

        if (selectedChat != owner)
            newMessage.gameObject.SetActive(false);
    }




    IEnumerator updateMessageList()
    {
        verticalLayoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        verticalLayoutGroup.enabled = true;

    }
}
