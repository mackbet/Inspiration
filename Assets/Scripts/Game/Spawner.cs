using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public static CharacterName name;
    [SerializeField] private List<Character> _characterPrefabs;

    [SerializeField] private Monster _monsterPrefab;

    [Header("Minimum distance to the monster")]
    [SerializeField] private float minDistance;

    private void Awake()
    {
        instance = this;
    }

    public static Monster SpawnMonster(Vector3 position)
    {
        return MasterManager.NetworkInstantiate(instance._monsterPrefab.gameObject, position, Quaternion.identity).GetComponent<Monster>();
    }

    public static Character SpawnPlayer()
    {
        GameObject newCharacter = instance._characterPrefabs[instance._characterPrefabs.FindIndex(x => x.name == name)].gameObject;
        while (true)
        {
            Vector3 position = ForestSpawner.GetRandomPosition(false);

            if (Vector3.Distance(position, Vector3.zero) > instance.minDistance)
            {
                newCharacter = MasterManager.NetworkInstantiate(newCharacter, position, Quaternion.identity);
                break;
            }

        }
        return newCharacter.GetComponent<Character>();
    }
}