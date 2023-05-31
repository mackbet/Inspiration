using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI _textField;
    [SerializeField] public string NickName { get; private set; }

    [SerializeField] GameObject _leaderIcon;
    [SerializeField] GameObject _readyIcon;

    public Player Player { get; private set; }
    public bool isReady = false;

    private string _characterName;

    [Header("Colors")]
    public string CharacterNameColor;

    public void SetPlayerInfo(Player player)
    {
        _leaderIcon.SetActive(player.IsMasterClient);
        isReady = player.IsMasterClient;

        Player = player;

        NickName = (string)player.CustomProperties["Nickname"];

        if(Player.CustomProperties.ContainsKey("CharacterName"))
            UpdateCharacterName();

        SetPlayerText(Player);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null && targetPlayer==Player)
        {
            if (changedProps.ContainsKey("CharacterName"))
                UpdateCharacterName();




            SetPlayerText(Player);
        }
    }

    private void SetPlayerText(Player player)
    {
        if(!player.IsMasterClient)
            _readyIcon.SetActive(isReady);


        _textField.text = $"{NickName}  <color=#{CharacterNameColor}>{_characterName}</color>";
    }

    private void UpdateCharacterName()
    {
        CharacterName characterName = (CharacterName)Player.CustomProperties["CharacterName"];
        if (characterName != CharacterName.None)
            _characterName = characterName.ToString();
        else
            _characterName = "";

    }

    public void SetIsReady(bool state)
    {
        isReady = state;
        SetPlayerText(Player);
    }
}
