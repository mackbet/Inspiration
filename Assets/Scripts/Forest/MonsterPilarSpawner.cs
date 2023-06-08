using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MonsterPilarSpawner : MonoBehaviourPunCallbacks
{
    public static MonsterPilarSpawner instance;
    [SerializeField] private EnvironmentObjectPrefab pilarPrefab;
    [field: SerializeField] public int pilarCount { get; private set; }
    [SerializeField] Transform container;

    [SerializeField] private float delay;
    private void Awake()
    {
        instance = this;
    }

    private List<MonsterPilar> pilars = new List<MonsterPilar>();
    public void SpawnPilars()
    {
        int count = 0;
        while (count < pilarCount)
        {
            Vector2Int pos = new Vector2Int(RandomHelper.GetRandomInt(5, ForestSpawner.instance.width - 5), RandomHelper.GetRandomInt(5, ForestSpawner.instance.height - 5));

            if (ForestSpawner.Map[pos.x, pos.y] == null)
            {
                GameObject newGO = Instantiate(pilarPrefab.models[0], ForestSpawner.GetPositionFromIndex(pos), Quaternion.identity, container);

                ForestSpawner.Map[pos.x, pos.y] = new EnvironmentObject(pilarPrefab, newGO, pos);

                Debug.Log(pilarPrefab.radius);

                ForestSpawner.MarkObjectRadius(ForestSpawner.Map[pos.x, pos.y], pilarPrefab.radius);

                MonsterPilar pilar = newGO.GetComponent<MonsterPilar>();
                pilar.onPilarActivated.AddListener(() => SendActivated(pilar));
                pilars.Add(pilar);

                count++;
            }
        }
    }
    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(delay);

        Monster monster = Spawner.SpawnMonster(transform.position + Vector3.forward);

        monster.GetComponent<Activator>().Activate();
    }

    public void SendActivated(MonsterPilar pilar)
    {
        base.photonView.RPC("RPC_MosterPilarActivated", RpcTarget.Others, pilar.transform.position);

        base.photonView.RPC("RPC_MosterPilarActivated", RpcTarget.MasterClient, pilar.transform.position);
    }

    [PunRPC]
    private void RPC_MosterPilarActivated(Vector3 position)
    {
        int index = pilars.FindIndex(x => x.transform.position == position);

        if (index!=-1)
        {
            pilars[index].onPilarActivated.RemoveAllListeners();

            pilars[index].Activate();
        }

    }

    [PunRPC]
    private void RPC_SpawnMoster(Vector3 position)
    {
        StartCoroutine(SpawnMonster());
    }
}