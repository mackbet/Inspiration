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
                Debug.Log(LayerMask.NameToLayer(targetLayer));
                go.layer = LayerMask.NameToLayer(targetLayer);

            }
        }
    }
}
