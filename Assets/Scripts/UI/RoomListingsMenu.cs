using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _container;
    [SerializeField] private RoomListing _roomListing;

    private List<RoomListing> _listings=new List<RoomListing>();

    public UnityEvent OnRoomJoined;
    public override void OnJoinedRoom()
    {
        _container.DestroyChildren();
        _listings.Clear();
        OnRoomJoined.Invoke();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);

            if (info.RemovedFromList)
            {
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }

            }
            else
            {

                if (index == -1)
                {
                    RoomListing listing = Instantiate(_roomListing, _container);
                    listing.SetRoomInfo(info);
                    _listings.Add(listing);
                }
                else
                {
                    _listings[index].SetRoomInfo(info);
                }
            }
        }
    }
}
