using Godot;
using System;
using EventCallback;

public class RedBlobCollision : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public void BodyEntered(Node body)
    {
        if (body.IsInGroup("Map"))
        {
            PlayAudioEvent paei = new PlayAudioEvent();
            paei.soundEffectType = SoundEffectType.HIT;
            paei.AudioTarget = (Node2D)GetParent();
            paei.FireEvent();
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
