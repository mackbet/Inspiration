using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviourPun
{
    [SerializeField] private Movement _movement;
    [SerializeField] private GameObject camera;
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(_movement);
            Destroy(camera);
        }
    }
}
