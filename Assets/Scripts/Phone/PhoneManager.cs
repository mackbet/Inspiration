using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    [SerializeField] private Image battareyFiller;
    [SerializeField] private Image signalFiller;

    private int[,] map;
    private bool isDisplayActive;
    [SerializeField] private int networkPointCount;
    [SerializeField] private int pointStartPower;

    public static float battery { get; private set; } = 100;
    public static int networkPower { get; private set; } = 0;

    public static UnityEvent onNetworkPowerChanged;

    [SerializeField] GameObject cube;

    private void Start()
    {
        CreateNetworkMap();
        StartCoroutine(work());
    }
    public void PhoneActivated()
    {
        isDisplayActive = true;
    }
    public void PhoneDeactivated()
    {
        isDisplayActive = false;
    }

    IEnumerator work()
    {
        while (true)
        {
            Vector3 pos = GM.playerPos;
            Vector2Int index = ForestSpawner.GetIndexFromPosition(pos);
            networkPower = map[index.x, index.y];

            if (isDisplayActive)
            {
                signalFiller.fillAmount = (float)networkPower / pointStartPower;

                battery -= 0.5f;
                battareyFiller.fillAmount = battery / 100;
            }

            onNetworkPowerChanged.Invoke();
            yield return new WaitForSeconds(2f);
        }
    }

    private void CreateNetworkMap()
    {
        map = new int[ForestSpawner.instance.width + 1, ForestSpawner.instance.height + 1];

        for (int i = 0; i < networkPointCount; i++)
        {
            Vector2Int index = ForestSpawner.GetIndexFromPosition(ForestSpawner.GetRandomPosition());

            Instantiate(cube, ForestSpawner.GetPositionFromIndex(index), Quaternion.identity);

            SetPoint(index, pointStartPower);
        }
    }

    private void SetPoint(Vector2Int index, int power)
    {
        if (!ForestSpawner.isValidIndex(index) || power < 0)
            return;

        if (map[index.x, index.y] < power)
        {
            map[index.x, index.y] = power;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Vector2Int delta = new Vector2Int(i, j);

                    if (delta != Vector2Int.zero)
                        SetPoint(index + delta, power - 1);
                }
            }
        }


    }
}


