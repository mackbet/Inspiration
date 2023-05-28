using System;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Rigidbody rb;

    [Header("Parameters")]
    [SerializeField] private float standSpeed = 5f;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private KeyCode crouchKey = KeyCode.C;

    [SerializeField] private bool cycledForest;


    public Action<Vector3> onMoving;
    public Action<MovementState> onStateChanged;

    //tools
    private Vector3 moveDirection;
    private float moveSpeed;


    [SerializeField] private MovementState _movementState = MovementState.crouching;

    private void Start()
    {
        SwitchMoveState();
    }


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveDirection = _camera.transform.right * moveHorizontal + _camera.transform.forward * moveVertical;
        moveDirection.y = 0;
        moveDirection.Normalize();

        if (Input.GetKeyDown(crouchKey))
            SwitchMoveState();


        if (cycledForest)
        {
            Vector3 currentPosition= rb.transform.position;
            float mapWidthLimit = ForestSpawner.instance.width / 2;
            float mapHeightLimit = ForestSpawner.instance.height / 2;
            if (currentPosition.x > mapWidthLimit)
                currentPosition.x = -mapWidthLimit;
            else if (currentPosition.x < -mapWidthLimit)
                currentPosition.x = mapWidthLimit;

            if (currentPosition.z > mapHeightLimit)
                currentPosition.z = -mapHeightLimit;
            else if (currentPosition.z < -mapHeightLimit)
                currentPosition.z = mapHeightLimit;

            if (currentPosition != rb.transform.position)
                rb.transform.position = currentPosition;
        }
    }

    private void FixedUpdate()
    {
        if (moveDirection.magnitude > 0)
        {
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
            onMoving.Invoke(rb.velocity);
        }
        else if (rb.velocity.magnitude > 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            onMoving.Invoke(Vector3.zero);
        }

    }

    private void SwitchMoveState()
    {
        if (_movementState == MovementState.standing)
        {
            _movementState = MovementState.crouching;

            moveSpeed = crouchSpeed;
        }
        else if (_movementState == MovementState.crouching)
        {
            _movementState = MovementState.standing;

            moveSpeed = standSpeed;
        }

        onStateChanged.Invoke(_movementState);
    }
}

public enum MovementState
{
    standing,
    crouching,
}
