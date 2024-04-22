using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private Vector2 movement;
    private GameObject player;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rigidBody2D.MovePosition(rigidBody2D.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void MoveUp()
    {
        movement.y = 1f;
    }

    public void MoveDown()
    {
        movement.y = -1f;
    }

    public void MoveLeft()
    {
        movement.x = -1f;
    }

    public void MoveRight()
    {
        movement.x = 1f;
    }

    public void StopMoving()
    {
        movement.x = 0f;
        movement.y = 0f;
    }
}
