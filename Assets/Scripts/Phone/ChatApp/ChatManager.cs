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
    [SerializeField] private UIAnimation ChatPage;
    [SerializeField] private TextMeshProUGUI ChatTitle;


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
        ChatTitle.text = (string)targetChat.Player.CustomProperties["Nickname"];
        ChatPage.Move();

        if (selectedChat != CharacterName.None && !ChatHistory.ContainsKey(selectedChat))
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

        if (PhoneManager.networkPower > 10)
            base.photonView.RPC("RPC_ChatMessage", selectedPlayer, inputField.text, myCharacter);
        else
            newMessage.ShowError();

        StartCoroutine(updateMessageList());

        inputField.text = "";
    }

    [PunRPC]
    private void RPC_ChatMessage(string text, CharacterName owner)
    {
        SavedMessage newSM = new SavedMessage(text, owner);
        if (PhoneManager.networkPower > 10)
        {
            AddMessage(newSM);
        }
        else
        {
            buffer.Add(newSM);
        }
    }

    private void AddMessage(SavedMessage sm)
    {
        Message newMessage = Instantiate(MessagePrefab, container);

        newMessage.Set—ompanionMessage(sm.text);

        ChatHistory[sm.owner].Add(newMessage);

        if (selectedChat != sm.owner)
            newMessage.gameObject.SetActive(false);

        StartCoroutine(updateMessageList());
    }


    List<SavedMessage> buffer=new List<SavedMessage>();

    private void CheckBuffer()
    {
        if (PhoneManager.networkPower < 10)
            return;

        foreach (SavedMessage sm in buffer)
        {
            AddMessage(sm);
        }

        buffer.Clear();
    }

    IEnumerator updateMessageList()
    {
        verticalLayoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        verticalLayoutGroup.enabled = true;

    }

    private class SavedMessage
    {
        public string text;
        public CharacterName owner;

        public SavedMessage(string _text, CharacterName _owner)
        {
            text = _text;
            owner = _owner;
        }
    }

}
