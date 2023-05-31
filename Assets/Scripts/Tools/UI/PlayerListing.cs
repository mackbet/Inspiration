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

    [SerializeField] GameObject _leaderIcon;
    [SerializeField] GameObject _readyIcon;

    public Player Player { get; private set; }
    public bool isReady = false;
    public void SetPlayerInfo(Player player)
    {
        if (player.IsMasterClient)
            _leaderIcon.SetActive(true);
        else
            _leaderIcon.SetActive(false);

        Player = player;

        SetPlayerText(Player);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null && targetPlayer==Player)
        {
            SetPlayerText(targetPlayer);
        }
    }

    private void SetPlayerText(Player player)
    {
        if(!player.IsMasterClient)
            _readyIcon.SetActive(isReady);

        string Nicknname = (string)player.CustomProperties["Nickname"];

        _textField.text = $"{Nicknname}";
    }

    public void SetIsReady(bool state)
    {
        isReady = state;
        SetPlayerText(Player);
    }
}
