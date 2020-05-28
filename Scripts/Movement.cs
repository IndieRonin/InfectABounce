using Godot;
using System;
using EventCallback;
public class Movement : RigidBody2D
{
    //The shot speed of the ball
    float speed = 1000;
    //The direction the force will be applied to the blob
    Vector2 moveDirection;
    //The start position of the click
    Vector2 dragStartPos;
    //The end position where the mouse button was released
    Vector2 dragEndPos;
    //dragging is active while the mouse buton is down until it has been realeased
    bool dragging = false;
    //If the dragging was coompleted succesfuly
    bool dragComplete = false;
    //If the draging action was cancled by clicking the right most button
    bool dragCanceled = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register with the input event system to listen to event messages
        InputCallbackEvent.RegisterListener(GrabInput);

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (dragging);

    }

    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        if (dragging) return;
        moveDirection = dragEndPos - dragStartPos;
        ApplyCentralImpulse(moveDirection * speed);
    }

    private void GrabInput(InputCallbackEvent icei)
    {
        if (icei.leftPressed)
        {
            GD.Print("Dragging mouse");
            dragging = true;
            dragStartPos = GetGlobalMousePosition();
        }
        if (icei.leftRelease && dragging)
        {
            GD.Print("Dragging mouse complete");
            dragComplete = true;
            dragging = false;
            dragStartPos = GetGlobalMousePosition();
        }
        if (icei.rightPressed && dragging)
        {
            GD.Print("Dragging mouse Cancled");
            dragCanceled = true;
            dragStartPos = Vector2.Zero;
            dragStartPos = Vector2.Zero;
            dragging = false;
        }
    }
}
