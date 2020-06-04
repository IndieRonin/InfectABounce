using Godot;
using System;
using EventCallback;

public class InputManager : Node2D
{
    bool upCheck, downCheck, leftCheck, rightCheck;
    ulong lastMousePosTimeEntry = 0;
    bool mouseUpdateCalled = false;

    public override void _Input(Godot.InputEvent @event)
    {
        if (@event is InputEventMouseButton || @event is InputEventKey)
        {
           InputCallbackEvent icei = new InputCallbackEvent();
            if (@event.IsActionPressed("LeftMouseButton")) icei.lmbClickPressed = true;
            if (@event.IsActionReleased("LeftMouseButton")) icei.lmbClickRelease = true;
            if (@event.IsActionPressed("RightMouseButton")) icei.rmbClickPressed = true;
            /*
            if (@event.IsActionReleased("RightMouseButton")) icei.rmbClickRelease = true;
            if (@event.IsActionPressed("MoveUp")) icei.upPressed = true;
            if (@event.IsActionReleased("MoveUp")) icei.upRelease = true;
            if (@event.IsActionPressed("MoveDown")) icei.downPressed = true;
            if (@event.IsActionReleased("MoveDown")) icei.downRelease = true;
            if (@event.IsActionPressed("MoveLeft")) icei.leftPressed = true;
            if (@event.IsActionReleased("MoveLeft")) icei.leftRelease = true;
            if (@event.IsActionPressed("MoveRight")) icei.rightPressed = true;
            if (@event.IsActionReleased("MoveRight")) icei.rightRelease = true;
            */
            icei.FireEvent();
        }
    }
}

    