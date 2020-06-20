using Godot;
using System;
using EventCallback;

public class GreenBlobMovement : RigidBody2D
{
    float speed = 20f;
    float maxSpeed = 200f;
    Node2D target = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        DeathEvent.RegisterListener(PlayerDeath);
    }

    public void Area2DBodyEntered(Node body)
    {
        if (body.IsInGroup("RedBlob"))
        {
            //Set the target to the red blob
            target = (Node2D)body;
        }
    }

    public void Area2DBodyExited(Node body)
    {
        if (body.IsInGroup("RebBlob"))
        {
            //Set the target to the red blob
            target = null;
        }
    }

    private void PlayerDeath(DeathEvent dei)
    {
        if (dei.target.IsInGroup("RedBlob"))
        {
            target = null;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        //If we dont have a target then return out of the method
        if (target == null) return;

        //Impliment movement here hahaha pproblem for future Gerrie? dont know yet
        Vector2 moveDir = target.Position - Position;
        moveDir = moveDir.Normalized();
        ApplyImpulse(moveDir, moveDir * speed);
        //AddForce(moveDir, moveDir * speed);

        if (Mathf.Abs(LinearVelocity.x) > maxSpeed || Mathf.Abs(LinearVelocity.y) > maxSpeed)
        {
            Vector2 newSpeed = LinearVelocity.Normalized();
            newSpeed *= maxSpeed;
            LinearVelocity = newSpeed;
        }

    }
}
