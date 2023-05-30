using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SeedManager : MonoBehaviourPunCallbacks
{
    int seed;
    public override void OnEnable()
    {
        base.OnEnable();

        if (PhotonNetwork.IsMasterClient)
        {
            seed = Random.Range(0, 1000);
            RandomHelper.SetSeed(seed);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }

    public void SendSeed(Player targetPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Got sent " + seed);
            base.photonView.RPC("RPC_ChangeSeed", targetPlayer, seed);
        }
    }
    [PunRPC]
    private void RPC_ChangeSeed(int seed)
    {
        Debug.Log("Got seed " + seed);
        RandomHelper.SetSeed(seed);
    }
}
