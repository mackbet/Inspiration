using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CharacterSelection : MonoBehaviourPunCallbacks
{
    [SerializeField] private SelectButton[] selectButtons;
    [SerializeField] private UIButton _selectedUIButton;

    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private void Start()
    {
        foreach (SelectButton sButton in selectButtons) {

            sButton.button.onClick.AddListener(() => { Select(sButton); });
        }
    }
    public override void OnEnable()
    {
        base.OnEnable();

        _selectedUIButton = null;
        foreach (SelectButton sButton in selectButtons)
        {
            sButton.button.DisableOutline();
        }
        Spawner.name = CharacterName.None;

        _myCustomProperties["CharacterName"] = CharacterName.None;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);

        UpdateSelectButtons();
    }

    public void Select(SelectButton sButton)
    {
        if (_selectedUIButton == sButton.button)
        {
            _selectedUIButton.DisableOutline();
            _selectedUIButton = null;

            Spawner.name = CharacterName.None;

            _myCustomProperties["CharacterName"] = CharacterName.None;
            PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);
        }
        else
        {
            if (_selectedUIButton != null)
            {
                _selectedUIButton.DisableOutline();
            }
            _selectedUIButton = sButton.button;
            _selectedUIButton.EnableOutline();

            Spawner.name = sButton.name;

            _myCustomProperties["CharacterName"] = sButton.name;
            PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        if (changedProps.ContainsKey("CharacterName"))
        {
            UpdateSelectButtons();
        }
    }

    private void UpdateSelectButtons()
    {
        List<CharacterName> selectedNames= new List<CharacterName>();

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player != PhotonNetwork.LocalPlayer)
            {
                CharacterName selectedName = (CharacterName)player.CustomProperties["CharacterName"];

                if (selectedName != CharacterName.None)
                    selectedNames.Add(selectedName);
            }
        }

        foreach (SelectButton sButton in selectButtons)
        {
            int index = selectedNames.FindIndex(x => x == sButton.name);

            if (index != -1)
                sButton.button.DisableButton();
            else
            {
                if(_selectedUIButton != sButton.button)
                {
                    sButton.button.EnableButton();
                }
            }
        }
    }

    [System.Serializable]
    public class SelectButton
    {
        [field: SerializeField] public CharacterName name { get; private set; }
        [field: SerializeField] public UIButton button { get; private set; }
    }
}
