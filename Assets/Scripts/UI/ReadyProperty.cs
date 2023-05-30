using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadyProperty : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    private bool isReady = false;

    private void Start()
    {
        _myCustomProperties["IsReady"] = isReady;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);
    }

    public void SetIsReady(bool state)
    {
        isReady = state;

        if (isReady)
            _textField.text = "Ready";
        else
            _textField.text = "Not ready";

        _myCustomProperties["IsReady"] = isReady;

        PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);
    }
    private void SwitchReady()
    {
        SetIsReady(!isReady);
    }


    public void OnClick_Button()
    {
        SwitchReady();
    }

    [PunRPC]
    private void RPC_ChangeReadyState(bool ready)
    {

    }
}
