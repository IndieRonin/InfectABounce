using Godot;
using System;
using EventCallback;

public class GreenBlobCollision : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        //Godot.Collections.Array col =  GetCollidingBodies();  
        //if(col.Count > 0) GD.Print("Hit detected fom the _Process methos");  
    }

    public void BodyEntered(Node body)
    {
        if (body.IsInGroup("RedBlob"))
        {
            HitEvent hei = new HitEvent();
            hei.target = (Node2D)body;
            hei.attacker = (Node2D)this;
            hei.damage = 50;
            hei.FireEvent();
        }
        if (body.IsInGroup("BlueBlob"))
        {
            if (((BlueBlob)body).Infected())
            {
                HitEvent hei = new HitEvent();
                hei.target = (Node2D)this;
                hei.attacker = (Node2D)body;
                hei.damage = 100;
                hei.FireEvent();
            }
        }
    }
}
