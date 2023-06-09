using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private List<MonsterTracker> players=new List<MonsterTracker>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MonsterTracker player))
        {
            players.Add(player);
            if (players.Count==GameManager.playersAlive)
            {
                PhotonNetwork.LoadLevel(0);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MonsterTracker player))
        {
            players.Remove(player);
        }
    }
}
