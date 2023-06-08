using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterTracker target;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;

    [SerializeField] private float minSearchCellDistance;
    [SerializeField] private int maxSearchCellDistance;
    [SerializeField] private int checkRadius;
    [SerializeField] private float attackDelay;

    //public Transform test;

    public GameObject scanedCellPrefab;

    public MonsterState state = MonsterState.idle;
    private Vector3 targetPosition;
    private MonsterMapCell[,] map;



    private float speed { get { return agent.speed; } set{ agent.speed = value; onMoving.Invoke(value); } }
    public Action<float> onMoving;
    public Action<MonsterState> onStateChanged;

    private void Start()
    {
        Initialize();

        SpawnMonster();
        SetState(MonsterState.searching);
    }

    private void Initialize()
    {
        map = ForestSpawner.MonsterMap;
    }

    private void FixedUpdate()
    {
        if (speed>0)
        {
            if (target != null)
            {
                targetPosition = target.transform.position;
                agent.SetDestination(targetPosition);
            }

            if (Vector3.Distance(targetPosition, transform.position) < 0.1f)
            {
                targetPosition = transform.position;

                if(state== MonsterState.patrolling)
                    SetState(MonsterState.scanning);
                else
                    SetState(MonsterState.searching);
            }

            ScanCellsAround();

            //test.position = targetPosition;
        }
    }

    private void SetState(MonsterState newState)
    {
        if (state == newState)
            return;

        state = newState;

        switch (state)
        {
            case MonsterState.idle:
                speed = 0;
                break;
            case MonsterState.attacking:
                speed = 0;
                break;
            case MonsterState.climbing:
                speed = 0;
                break;
            case MonsterState.scanning:
                Scan();
                break;
            case MonsterState.patrolling:
                speed = patrolSpeed;
                break;
            case MonsterState.chasing:
                speed = chaseSpeed;
                break;
            case MonsterState.searching:
                speed = 0;
                Search();
                break;
        }

        onStateChanged.Invoke(state);
    }

    private void SpawnMonster()
    {
        Vector2Int currentIndex = ForestSpawner.GetIndexFromPosition(transform.position);

        transform.position = ForestSpawner.GetPositionFromIndex(ChooseCell(currentIndex, 1));
    }



    #region Patrolling
    private void Scan()
    {
        Vector2Int currentPosIndex = ForestSpawner.GetIndexFromPosition(transform.position);

        targetPosition = ForestSpawner.GetPositionFromIndex(ChooseCell(currentPosIndex, maxSearchCellDistance));

        agent.SetDestination(targetPosition);

        SetState(MonsterState.patrolling);

    }
    private Vector2Int ChooseCell(Vector2Int currentPosIndex, int scale)
    {
        List<Vector2Int> enableCells = new List<Vector2Int>();

        for (int i = -scale; i <= scale; i++)
        {
            for (int j = -scale; j <= scale; j++)
            {
                Vector2Int currentCell = currentPosIndex + new Vector2Int(i, j);

                if (ForestSpawner.isValidIndex(currentCell) && map[currentCell.x, currentCell.y] == MonsterMapCell.empty && Vector2Int.Distance(currentCell, currentPosIndex) > minSearchCellDistance)
                {
                    enableCells.Add(currentCell);
                    break;
                }
            }
        }
        if (enableCells.Count > 0)
        {
            return enableCells[Random.Range(0, enableCells.Count)];
        }
        else
        {

            if (scale > ForestSpawner.instance.width && scale > ForestSpawner.instance.height)
                ResetMap();

            return ChooseCell(currentPosIndex, scale + 1);
        }
    }

    private void ResetMap()
    {
        for(int i = 0; i< map.GetLength(0);i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == MonsterMapCell.scanned)
                    map[i, j] = MonsterMapCell.empty;
            }
        }
    }
    private void ScanCellsAround()
    {
        Vector2Int currentIndex = ForestSpawner.GetIndexFromPosition(transform.position);
        for (int i = -checkRadius; i <= checkRadius; i++)
        {
            for (int j = -checkRadius; j <= checkRadius; j++)
            {
                Vector2Int index = currentIndex + new Vector2Int(i, j);

                if (ForestSpawner.isValidIndex(index) && map[index.x, index.y] == MonsterMapCell.empty)
                {
                    map[index.x, index.y] = MonsterMapCell.scanned;
                    if(scanedCellPrefab)
                        Instantiate(scanedCellPrefab, ForestSpawner.GetPositionFromIndex(index), Quaternion.identity);
                }
            }
        }
    }
    #endregion

    #region Search Player
    private void Search()
    {
        if (lookAroundCoroutine != null)
            lookAroundCoroutine = null;

        lookAroundCoroutine = StartCoroutine(LookAround());
    }
    private Coroutine lookAroundCoroutine;
    IEnumerator LookAround()
    {
        float delay = 6f;
        float targetRotation = 180.0f;
        Quaternion startRotation;
        Quaternion targetQuaternion;

        startRotation = transform.rotation;
        targetQuaternion = Quaternion.Euler(transform.eulerAngles + new Vector3(0.0f, targetRotation, 0.0f));

        float t = 0.0f;
        while (t < 1.0f)
        {
            if (target != null)
                yield break;

            t += Time.deltaTime / delay;

            transform.rotation = Quaternion.Lerp(startRotation, targetQuaternion, t);

            yield return null;
        }

        SetState(MonsterState.scanning);

        lookAroundCoroutine = null;
    }
    public void SetTarget(MonsterTracker newTarget)
    {
        if (target != newTarget)
        {
            target = newTarget;

            if (newTarget != null)
                SetState(MonsterState.chasing);
        }
    }
    public void Attack(MonsterTracker victim)
    {
        victim.Dead();
        StartCoroutine(attack());
    }
    private IEnumerator attack()
    {
        agent.isStopped = true;
        SetState(MonsterState.attacking);

        yield return new WaitForSeconds(attackDelay);

        agent.isStopped = false;
        SetState(MonsterState.searching);
    }
    #endregion

}

public enum MonsterMapCell
{
    empty,
    obstacle,
    scanned,
}

public enum MonsterState
{
    idle,//проста стоит
    patrolling,//идет чекать точку
    chasing,//бежит за игроком
    attacking,//бьёт
    climbing,
    scanning,//думает куда пойти
    searching,//осматривается
} 
