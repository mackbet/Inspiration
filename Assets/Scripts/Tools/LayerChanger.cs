using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviourPun
{
    [SerializeField] GameObject[] objects;
    [SerializeField] string targetLayer;
    void Start()
    {
        if (photonView.IsMine)
        {
            foreach(GameObject go in objects)
            {
                go.layer = LayerMask.NameToLayer(targetLayer);

            }
        }
    }
}
