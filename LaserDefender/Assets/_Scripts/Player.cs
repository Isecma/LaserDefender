using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Shooter shooter;

    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    void Awake()
    {
     shooter = GetComponent<Shooter>();   
    }

    void Start()
    {
        InitialBounds();    
    }

    void Update()
    {
        Move();
    }

    void InitialBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector3 playerMovement = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(transform.position.x + playerMovement.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + playerMovement.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPosition;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
