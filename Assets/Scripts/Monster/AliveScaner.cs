using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveScaner : MonoBehaviour
{
    [SerializeField] private Transform headTransform;

    [Range(0f, 180f)]
    [SerializeField] private float viewingAngle;
    [Range(0f, 30f)]
    [SerializeField] private float viewingRange;

    [SerializeField] private LayerMask forestLayer;

    [field: SerializeField] public MonsterTracker[] trackers { get; set; }
    [SerializeField] private Monster _monster;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        trackers = FindObjectsOfType<MonsterTracker>();
    }

    void Update()
    {
        CheckAlives();
    }

    private void CheckAlives()
    {
        //DrawGazeDirection();

        float minDistance = viewingRange;
        MonsterTracker selectedTarget = null;

        foreach (MonsterTracker tracker in trackers)
        {
            if (tracker == null)
                continue;

            Vector3 directionToTarget = tracker.transform.position - headTransform.position;
            directionToTarget.y = 0;
            directionToTarget.Normalize();

            float angle = Vector3.Angle(headTransform.forward, directionToTarget);
            float distance = Vector3.Distance(tracker.transform.position, headTransform.position);
            if (angle < viewingAngle / 2 && distance < viewingRange && !isHidden(tracker))
            {
                if (selectedTarget == null || distance < minDistance)
                {
                    selectedTarget = tracker;
                }

            }

        }
        _monster.SetTarget(selectedTarget);
    }

    private bool isHidden(MonsterTracker tracker) {

        foreach(Transform tag in tracker.tags)
        {
            Vector3 direction = tag.position - headTransform.position;
            direction.Normalize();

            float distance = Vector3.Distance(tag.position, headTransform.position);


            RaycastHit hit;
            if (Physics.Raycast(headTransform.position, direction, out hit, distance, forestLayer))
            {
                Debug.DrawRay(headTransform.position, direction * distance, Color.red);
                continue;
            }
            else
            {
                Debug.DrawRay(headTransform.position, direction * distance, Color.green);
                return false;
            }
        }
        return true;
    }

    private void DrawGazeDirection()
    {
        Vector3 startPos = headTransform.position;
        Vector3 direction = headTransform.forward;
        direction.y = 0;


        Debug.DrawRay(startPos, RotateYVector3(direction, viewingAngle / 2) * viewingRange, Color.yellow);


        Debug.DrawRay(startPos, RotateYVector3(direction, viewingAngle / -2) * viewingRange, Color.yellow);
    }

    public Vector3 RotateYVector3(Vector3 vector, float angleDegrees)
    {
        Quaternion rotation = Quaternion.Euler(0f, angleDegrees, 0f);

        return rotation * vector;
    }
}
