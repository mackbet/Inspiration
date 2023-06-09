using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] private Portal portalPrefab;

    private Portal portal;

    private void Start()
    {
        Vector2Int pos = new Vector2Int(RandomHelper.GetRandomInt(5, ForestSpawner.instance.width - 5), RandomHelper.GetRandomInt(5, ForestSpawner.instance.height - 5));

        portal = Instantiate(portalPrefab, ForestSpawner.GetPositionFromIndex(pos), Quaternion.identity);
    }

   public void ActivatePortal()
    {
        portal.gameObject.SetActive(true);
    }
}
