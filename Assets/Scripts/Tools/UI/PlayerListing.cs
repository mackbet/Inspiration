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

    public Player Player { get; private set; }
    public bool isReady = false;
    public void SetPlayerInfo(Player player)
    {
        if (player.IsMasterClient)
            isReady = true;

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
        _textField.text = $"{player.NickName}({isReady}))";
    }

    public void SetIsReady(bool state)
    {
        isReady = state;
        SetPlayerText(Player);
    }
}
