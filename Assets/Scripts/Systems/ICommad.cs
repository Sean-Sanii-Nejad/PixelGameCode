using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface ICommand
{
    void Execute();
}

public class CommandMove: ICommand
{
    private Rigidbody2D rigidBody2D;
    private AttributeSet attributeSet;
    private Animator animator;
    private Vector2 movement;

    public CommandMove(Rigidbody2D rigidBody2D, Animator animator, AttributeSet attributeSet, Vector2 movement)
    {
        this.rigidBody2D = rigidBody2D;
        this.animator = animator;
        this.attributeSet = attributeSet;
        this.movement = movement;
    }

    public void Execute()
    {
        UpdateAnimator();
        MovePlayer();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void MovePlayer()
    {
        rigidBody2D.MovePosition(rigidBody2D.position + movement.normalized * attributeSet.GetSpeed() * Time.fixedDeltaTime);
    }
}

public class CommandMoveStop : ICommand
{
    private Rigidbody2D rigidBody2D;
    private AttributeSet attributeSet;
    private Animator animator;
    private Vector2 movement;
    private PlayerController playerController;

    public CommandMoveStop(Rigidbody2D rigidBody2D, Animator animator, AttributeSet attributeSet, PlayerController playerController)
    {
        this.rigidBody2D = rigidBody2D;
        this.animator = animator;
        this.attributeSet = attributeSet;
        this.playerController = playerController;
    }

    public void Execute()
    {
        movement.x = 0f;
        movement.y = 0f;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        rigidBody2D.MovePosition(rigidBody2D.position + movement.normalized * attributeSet.GetSpeed() * Time.fixedDeltaTime);
        playerController.SetStop(false);
    }
}

public class CommandInteract : ICommand
{
    public void Execute()
    {

    }
}

public class CommandBack : ICommand
{
    public void Execute()
    {

    }
}

public static class Invoker
{
    private static Queue<ICommand> commandQueue = new Queue<ICommand>();

    public static void addCommand(ICommand command)
    {
        commandQueue.Enqueue(command);
    }

    public static void nextCommand()
    {
        ICommand command = commandQueue.Dequeue();
        command.Execute();
    }
}

//public class CommandMoveDown : ICommand
//{
//    private Rigidbody2D rigidBody2D;
//    private AttributeSet attributeSet;
//    private Animator animator;
//    private Vector2 movement;

//    public CommandMoveDown(Rigidbody2D rigidBody2D, Animator animator, AttributeSet attributeSet)
//    {
//        this.rigidBody2D = rigidBody2D;
//        this.animator = animator;
//        this.attributeSet = attributeSet;
//    }

//    public void Execute()
//    {
//        movement.y = -1f;
//        animator.SetFloat("Horizontal", movement.x);
//        animator.SetFloat("Vertical", movement.y);
//        animator.SetFloat("Speed", movement.sqrMagnitude);
//        rigidBody2D.MovePosition(rigidBody2D.position + movement.normalized * attributeSet.GetSpeed() * Time.fixedDeltaTime);
//    }
//}

//public class CommandMoveLeft : ICommand
//{
//    private Rigidbody2D rigidBody2D;
//    private AttributeSet attributeSet;
//    private Animator animator;
//    private Vector2 movement;

//    public CommandMoveLeft(Rigidbody2D rigidBody2D, Animator animator, AttributeSet attributeSet)
//    {
//        this.rigidBody2D = rigidBody2D;
//        this.animator = animator;
//        this.attributeSet = attributeSet;
//    }

//    public void Execute()
//    {
//        movement.x = -1f;
//        animator.SetFloat("Horizontal", movement.x);
//        animator.SetFloat("Vertical", movement.y);
//        animator.SetFloat("Speed", movement.sqrMagnitude);
//        rigidBody2D.MovePosition(rigidBody2D.position + movement.normalized * attributeSet.GetSpeed() * Time.fixedDeltaTime);
//    }
//}

//public class CommandMoveRight : ICommand
//{
//    private Rigidbody2D rigidBody2D;
//    private AttributeSet attributeSet;
//    private Animator animator;
//    private Vector2 movement;

//    public CommandMoveRight(Rigidbody2D rigidBody2D, Animator animator, AttributeSet attributeSet)
//    {
//        this.rigidBody2D = rigidBody2D;
//        this.animator = animator;
//        this.attributeSet = attributeSet;
//    }

//    public void Execute()
//    {
//        movement.x = 1f;
//        animator.SetFloat("Horizontal", movement.x);
//        animator.SetFloat("Vertical", movement.y);
//        animator.SetFloat("Speed", movement.sqrMagnitude);
//        rigidBody2D.MovePosition(rigidBody2D.position + movement.normalized * attributeSet.GetSpeed() * Time.fixedDeltaTime);
//    }
//}
