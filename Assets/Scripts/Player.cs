using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    private Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start() 
    {
        InitBounds();
    }

    void Update()
    {
        Move();
        
    }
    
    void InitBounds()
    {
        // setting the boundary for the player 
        Camera mainCamera = Camera.main;
        //left bottom corner of camera viewport is 0,0
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        //lRight top corner of camera viewport is 1,1
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    void Move()
    {
        // Using Time.deltaTime makes the movement framerate independant
        Vector2 delta = rawInput * (moveSpeed * Time.deltaTime);
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
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
