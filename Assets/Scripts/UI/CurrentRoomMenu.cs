using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;
using TMPro;
using Photon.Realtime;

public class CurrentRoomMenu : MonoBehaviourPunCallbacks
{

    [SerializeField] private PlayerListingsMenu playerListingsMenu;


    [SerializeField] private TextMeshProUGUI _roomNameField;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _readyButton;
    [SerializeField] private TextMeshProUGUI _readyButtonText;

    public UnityEvent onLeftRoom;

    private bool _ready = false;
    private bool _isMasterClient;

    public override void OnEnable()
    {
        base.OnEnable();

        _isMasterClient = PhotonNetwork.IsMasterClient;
        _startButton.SetActive(_isMasterClient);
        _readyButton.SetActive(!_isMasterClient);

        playerListingsMenu.Show();

        _roomNameField.text = PhotonNetwork.CurrentRoom.Name;
    }


    private void SetIsReady(bool state)
    {
        _ready = state;
        if (_ready)
            _readyButtonText.text = "Ready";
        else
            _readyButtonText.text = "Not ready";


        List<PlayerListing> playerListings = playerListingsMenu._listings;
        int index = playerListings.FindIndex(x => x.Player == PhotonNetwork.LocalPlayer);

        if (index != -1)
        {
            playerListings[index].SetIsReady(_ready);
        }
    }


    public void OnButtonClick_StartGame()
    {
        if (_isMasterClient)
        {
            List<PlayerListing> playerListings = playerListingsMenu._listings;

            foreach (PlayerListing listing in playerListings)
            {
                if(!listing.isReady)
                {
                    return;
                }
            }


            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(3);
        }
    }

    public void OnButtonClick_Leave()
    {
        PhotonNetwork.LeaveRoom(true);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        OnButtonClick_Leave();
    }

    public override void OnLeftRoom()
    {
        playerListingsMenu.Hide();
        onLeftRoom.Invoke();
    }

    public void OnButtonClick_IsReady()
    {
        if (!_isMasterClient)
        {
            SetIsReady(!_ready);

            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);

        }
    }


    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        List<PlayerListing> playerListings = playerListingsMenu._listings;
        int index = playerListings.FindIndex(x => x.Player == player);

        if (index != -1)
        {
            playerListings[index].SetIsReady(ready);
        }
    }
}
