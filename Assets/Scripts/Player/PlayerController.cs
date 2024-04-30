using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private Vector2 movement;
    private GameObject player;
    private AttributeSet attributeSet;
    private bool bMoveUp;
    private bool bMoveDown;
    private bool bMoveLeft;
    private bool bMoveRight;
    private bool bMoveStop;

    // Systems Controllers
    private ICommand command;

    void Awake()
    {
        player = GameObject.Find("Player");
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attributeSet = GetComponent<AttributeSet>();
    }

    void FixedUpdate()
    {
        if (bMoveStop)
        {
            CommandMoveStop commandMove = new CommandMoveStop(this.rigidBody2D, this.animator, this.attributeSet, this);
            Invoker.addCommand(commandMove);
            Invoker.nextCommand();
        }
        if (bMoveUp)
        {
            CommandMove commandMove = new CommandMove(this.rigidBody2D, this.animator, this.attributeSet, Vector2.up);
            Invoker.addCommand(commandMove);
            Invoker.nextCommand();
        }
        if (bMoveDown)
        {
            CommandMove commandMove = new CommandMove(this.rigidBody2D, this.animator, this.attributeSet, Vector2.down);
            Invoker.addCommand(commandMove);
            Invoker.nextCommand();
        }
        if(bMoveRight)
        {
            CommandMove commandMove = new CommandMove(this.rigidBody2D, this.animator, this.attributeSet, Vector2.right);
            Invoker.addCommand(commandMove);
            Invoker.nextCommand();
        }
        if(bMoveLeft)
        {
            CommandMove commandMove = new CommandMove(this.rigidBody2D, this.animator, this.attributeSet, Vector2.left);
            Invoker.addCommand(commandMove);
            Invoker.nextCommand();
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }
    
    public void SetStop(bool moveStop)
    {
        bMoveUp = moveStop;
        bMoveDown = moveStop;
        bMoveLeft = moveStop;
        bMoveRight = moveStop;
        bMoveStop = moveStop;
    }

    public void MoveUp()
    {
        bMoveUp = true;
    }

    public void MoveDown()
    {
        bMoveDown = true;
    }

    public void MoveLeft()
    {
        bMoveLeft = true;
    }

    public void MoveRight()
    {
        bMoveRight = true;
    }

    public void StopMoving()
    {
        bMoveStop = true;
    }
}
