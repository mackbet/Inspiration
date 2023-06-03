using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class ChatItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private TextMeshProUGUI lastMessageField;

    [SerializeField] private CharacterIcons[] images;

    Player Player;

    public void SetPlayer(Player player)
    {
        Player = player;

        nameField.text = (string)player.CustomProperties["Nickname"];
    }


    [System.Serializable]
    private class CharacterIcons
    {
        [SerializeField] private CharacterName name;
        [SerializeField] private Sprite characterSprite;
    }

}
