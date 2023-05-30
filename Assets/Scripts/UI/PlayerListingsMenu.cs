using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _container;
    [SerializeField] private PlayerListing _playerListing;

    public List<PlayerListing> _listings { get; private set; } = new List<PlayerListing>();

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom.Players == null || PhotonNetwork.CurrentRoom == null)
            return;

        var collection = PhotonNetwork.CurrentRoom.Players;
        Debug.Log("GetCurrentRoomPlayers");
        foreach (KeyValuePair<int,Player> playerInfo in collection)
        {
            Debug.Log(playerInfo.Value.NickName);
            AddPlayerListing(playerInfo.Value);
        }
        
    }
    private void AddPlayerListing(Player newPlayer)
    {
        PlayerListing listing = Instantiate(_playerListing, _container);

        listing.SetPlayerInfo(newPlayer);

        _listings.Add(listing);

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player.NickName == otherPlayer.NickName);

        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public void Show()
    {
        GetCurrentRoomPlayers();
    }
    public void Hide()
    {
        _container.DestroyChildren();
        _listings.Clear();
    }
}
