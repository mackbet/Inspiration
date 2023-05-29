using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListing : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textField;

    public Player Player { get; private set; }
    public void SetPlayerInfo(Player player)
    {
        Player = player;

        _textField.text = player.NickName;
    }
}
