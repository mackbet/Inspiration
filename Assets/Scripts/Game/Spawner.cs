using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawner : MonoBehaviour
{
    public static CharacterName name;
    [SerializeField] private List<Character> _characterPrefabs;

    [SerializeField] private Monster _monsterPrefab;

    [Header("Minimum distance to the monster")]
    [SerializeField] private float minDistance;


    public void SpawnMonster()
    {
        MasterManager.NetworkInstantiate(_monsterPrefab.gameObject, Vector3.zero, Quaternion.identity);
    }

    public Character SpawnPlayer()
    {
        GameObject newCharacter = _characterPrefabs[_characterPrefabs.FindIndex(x => x.name == name)].gameObject;
        while (true)
        {
            Vector3 position = ForestSpawner.GetRandomPosition(false);

            if (Vector3.Distance(position, Vector3.zero) > minDistance)
            {
                newCharacter = MasterManager.NetworkInstantiate(newCharacter, position, Quaternion.identity);
                break;
            }

        }
        return newCharacter.GetComponent<Character>();
    }
}