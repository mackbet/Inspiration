using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class ForestSpawner : MonoBehaviour
{
    public static ForestSpawner instance;

    [SerializeField] private NavMeshSurface surface;
    [field: SerializeField] public int width { get; private set; }
    [field: SerializeField] public int height { get; private set; }


    [Range(0f, 0.1f)]
    [SerializeField] private float objectPercentage;

    [SerializeField] private EnvironmentObjectPrefab[] eObjectPrefabs;

    [SerializeField] private Transform container;

    [field:SerializeField] public ForestAudio audio { get; private set; }
    public EnvironmentObject[,] Map { get; private set; }
    public MonsterMapCell[,] MonsterMap { get; private set; }
    public Dictionary<ObjectType, List<EnvironmentObject>> objectDictionary;

    [SerializeField] private Transform wallContainer;
    public GameObject[] walls;
    public int step;


    private int scale = 1;
    private int totalWeight = 0;

    void Awake()
    {
        instance = this;

        Initialize();
    }

    private void Initialize()
    {
        totalWeight = 0;
        Map = new EnvironmentObject[width * scale + 1, height * scale + 1];
        MonsterMap = new MonsterMapCell[width * scale + 1, height * scale + 1];

        objectDictionary = new Dictionary<ObjectType, List<EnvironmentObject>>();
        foreach (EnvironmentObjectPrefab eObj in eObjectPrefabs)
        {
            totalWeight += eObj.weight;
            eObj.weight = totalWeight;

            if(!objectDictionary.ContainsKey(eObj.type))
                objectDictionary.Add(eObj.type, new List<EnvironmentObject>());
        }
    }

    public void SpawnForest()
    {
        for (int i = 0; i < width * scale + 1; i++)
        {
            for (int j = 0; j < height * scale + 1; j++)
            {
                if (!isValidIndex(new Vector2Int(i, j)))
                    continue;

                int isEnvObject = RandomHelper.GetRandomInt(0, 100);
                if (isEnvObject < objectPercentage * 100)
                {
                    Vector3 newPos = GetPositionFromIndex(new Vector2Int(i, j));
                    EnvironmentObjectPrefab eObj = GetRandomEnvironmentObject();

                    GameObject newGO = Instantiate(eObj.models[RandomHelper.GetRandomInt(0, eObj.models.Length)], newPos, Quaternion.identity, container);

                    Map[i, j] = new EnvironmentObject(eObj, newGO, new Vector2Int(i, j));
                    MarkObjectRadius(Map[i, j], eObj.radius);

                    MonsterMap[i, j] = MonsterMapCell.obstacle;

                    objectDictionary[eObj.type].Add(Map[i, j]);

                }
            }
        }

        BakeNavMesh();
    }
    private void MarkObjectRadius(EnvironmentObject obj, int radius)
    {
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                Vector2Int currentIndex = obj.indices + new Vector2Int(i, j);
                if (isValidIndex(currentIndex))
                {
                    Map[currentIndex.x, currentIndex.y] = obj;
                    MonsterMap[currentIndex.x, currentIndex.y] = MonsterMapCell.obstacle;
                }
            }
        }
    }

    private void BakeNavMesh()
    {
        surface.BuildNavMesh();
    }
    #region Static Methods
    public EnvironmentObjectPrefab GetRandomEnvironmentObject()
    {
        int rand = RandomHelper.GetRandomInt(0, totalWeight);

        foreach (EnvironmentObjectPrefab eObj in eObjectPrefabs)
        {
            if(rand< eObj.weight)
                return eObj;
        }

        return eObjectPrefabs[0];
    }
    public static bool isValidIndex(Vector2 index)
    {
        if (index.x <= 0 || index.x >= instance.width * instance.scale)
            return false;

        if (index.y <= 0 || index.y >= instance.height * instance.scale)
            return false;

        return true;
    }
    public static Vector2Int GetIndexFromPosition(Vector3 position)
    {
        Vector3 temp = position - (instance.transform.position - new Vector3(instance.width / 2, 0, instance.height / 2));
        int x = (int)temp.x * instance.scale;
        int y = (int)temp.z * instance.scale;
        return new Vector2Int(Mathf.Clamp(x, 1, instance.width * 2), Mathf.Clamp(y, 1, instance.height * 2));
    }
    public static Vector3 GetPositionFromIndex(Vector2Int index)
    {
        return instance.transform.position - new Vector3(instance.width / 2, 0, instance.height / 2) + new Vector3(index.x / instance.scale, 0, index.y / instance.scale);
    }
    public static Vector3 GetRandomPosition()
    {
        return GetPositionFromIndex(new Vector2Int(RandomHelper.GetRandomInt(1, instance.width * instance.scale), RandomHelper.GetRandomInt(1, instance.height * instance.scale)));
    }
    public static Vector3 GetRandomPosition(ObjectType type)
    {
        List<EnvironmentObject> list;
        if (instance.objectDictionary.TryGetValue(type, out list) && list.Count > 0)
        {
            return GetPositionFromIndex(list[RandomHelper.GetRandomInt(0, list.Count)].indices);
        }
        else
            return GetRandomPosition();
    }
    #endregion
}