using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviourPun
{
    [SerializeField] GameObject[] objects;
    [SerializeField] string targetLayer;

    public void ChangeLayer()
    {
        foreach (GameObject go in objects)
        {
            go.layer = LayerMask.NameToLayer(targetLayer);
        }
    }
}
