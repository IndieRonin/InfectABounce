using Godot;
using System;

public class GreenBlobMovement : RigidBody2D
{
    Node2D target;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void Area2DBodyEntered(Node body)
    {
        if (body.IsInGroup("RebBlob"))
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        //If we dont have a target then return out of the method
        if(target == null) return;

        //Impliment movement here hahaha pproblem for future Gerrie? dont know yet

    }
}
