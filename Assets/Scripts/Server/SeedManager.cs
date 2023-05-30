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
        SendSeed(newPlayer);
    }

    public void SendSeed(Player targetPlayer)
    {
        if (!targetPlayer.IsMasterClient)
        {
            base.photonView.RPC("RPC_ChangeSeed", targetPlayer, seed);
        }
    }
    [PunRPC]
    private void RPC_ChangeSeed(int seed)
    {
        RandomHelper.SetSeed(seed);
    }
}
