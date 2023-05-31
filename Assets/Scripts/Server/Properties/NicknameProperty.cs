using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class NicknameProperty : MonoBehaviour
{
    [SerializeField] private TMP_InputField _textField;
    [SerializeField] private string Nickname;

    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private void Awake()
    {
        string savedValue = PlayerPrefs.GetString("Nickname");

        if (savedValue == string.Empty)
            _textField.text = MasterManager.GameSettings.NickName;
        else
            _textField.text = savedValue;

        SetNickName();
    }

    public void SetNickName()
    {
        if (_textField.text == string.Empty)
            _textField.text = Nickname;
        else
        {
            Nickname = _textField.text;

            PlayerPrefs.SetString("Nickname", Nickname);

            _myCustomProperties["Nickname"] = Nickname;

            PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);
        }
    }
}
