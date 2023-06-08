using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class MosterPilar : Interaction
{
    [SerializeField] private float delay;

    [SerializeField] private Action<Vector3> onActivated;

    public UnityEvent onPilarActivated;
    public override void Activate()
    {
        if (isActivated)
            return;

        SendActivated();
    }

    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(delay);

        Monster monster =  Spawner.SpawnMonster(transform.position + Vector3.forward);

        monster.GetComponent<Activator>().Activate();
    }
    public void SendActivated()
    {
        base.photonView.RPC("RPC_MosterPilarActivated", RpcTarget.All, transform.position);
    }

    [PunRPC]
    private void RPC_MosterPilarActivated(Vector3 position)
    {
        if (transform.position != position)
            return;

        SetOutline(false);
        isActivated = true;

        onPilarActivated.Invoke();

        if (!PhotonNetwork.IsMasterClient)
            return;

        StartCoroutine(SpawnMonster());
    }
}
