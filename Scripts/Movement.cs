using Godot;
using System;
using EventCallback;
public class Movement : RigidBody2D
{
    //The shot speed of the ball
    float speed = 1000f;

    float maxSpeed = 5f;
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

    public override void _PhysicsProcess(float delta)
    {
        if (dragging)
        {
            //We continuesly update the start position for our force add direction to the position the blob is
            dragStartPos = GlobalPosition;
        }
        //If the dragging has not completed we just exit out of hte method
        if (!dragComplete) return;
        moveDirection = dragEndPos - dragStartPos;
        moveDirection = moveDirection.Normalized();
        ApplyCentralImpulse(moveDirection * speed);
        dragging = false;
        dragComplete = false;

        //Bug or feature? Tis piece of code forces the red blob to stop before being able to shoot of in a direction again
        if (Mathf.Abs(LinearVelocity.x) > maxSpeed || Mathf.Abs(LinearVelocity.y) > maxSpeed)
        {
            Vector2 newSpeed = LinearVelocity.Normalized();
            newSpeed *= maxSpeed;
            LinearVelocity = newSpeed;
        }
    }

    private void GrabInput(InputCallbackEvent icei)
    {
        if (icei.lmbClickPressed)
        {
            dragging = true;
            dragComplete = false;
        }
        if (icei.lmbClickRelease && dragging)
        {
            dragComplete = true;
            dragEndPos = GetGlobalMousePosition();
        }
        if (icei.rmbClickPressed && dragging)
        {
            dragCanceled = true;
            dragStartPos = Vector2.Zero;
            dragEndPos = Vector2.Zero;
            dragging = false;
            dragComplete = false;
        }
    }

    public override void _ExitTree()
    {
        InputCallbackEvent.UnregisterListener(GrabInput);
    } 
}
