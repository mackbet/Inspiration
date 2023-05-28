using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnvironmentObject
{
    [SerializeField] public string Name;
    [field: SerializeField] public ObjectType type { get; protected set; }
    [field: SerializeField] public GameObject model { get; protected set; }
    public Vector2Int indices { get; protected set; }

    public EnvironmentObject(EnvironmentObjectPrefab copy, GameObject _model, Vector2Int _indices)
    {
        Name = copy.Name;
        type = copy.type;
        model = _model;
        indices = _indices;
    }
}

[Serializable]
public class EnvironmentObjectPrefab
{
    [SerializeField] public string Name;
    [field: SerializeField] public ObjectType type { get; private set; }
    [field: SerializeField] public int radius { get; private set; }
    [field: SerializeField] public int weight { get; set; }
    [field: SerializeField] public GameObject[] models { get; private set; }
}

public enum ObjectType
{
    Tree,
    Bush,
    Rock,
    BigRock,
}

public class Alive
{
    public Alive()
    {

    }
}
public class Person : Alive
{

}
