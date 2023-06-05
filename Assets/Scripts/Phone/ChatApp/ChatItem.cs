using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.Events;

public class ChatItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private TextMeshProUGUI lastMessageField;
    [SerializeField] private Button button;

    [SerializeField] private CharacterIcons[] images;

    public UnityEvent onClick;

    public Player Player { get; private set; }
    public CharacterName Character { get; private set; }

    private void Start()
    {
        button.onClick.AddListener(() => { onClick.Invoke(); });
    }

    public void SetPlayer(Player player)
    {
        Player = player;

        nameField.text = (string)player.CustomProperties["Nickname"];

        Character = (CharacterName)player.CustomProperties["CharacterName"];

        Debug.Log(Character);
        image.sprite = images[images.ToList().FindIndex(x => x.name == Character)].characterSprite;
    }


    [System.Serializable]
    private class CharacterIcons
    {
        public CharacterName name;
        public Sprite characterSprite;
    }

}
