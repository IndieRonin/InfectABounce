using Godot;
using System;
using EventCallback;
public class BlueBlobCollision : RigidBody2D
{
    //Set to true if the blue blob was touched by the red blob
    bool infected = false;
    //Sprite used to show that the blob is not yet infected
    Sprite blueSprite;
    //Sprite used to show the blob is infected
    Sprite redSprite;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Assign the sprites to be used by rthe blob
        blueSprite = GetNode<Sprite>("BlueSprite");
        redSprite = GetNode<Sprite>("RedSprite");
        //Hide the red sprite when the node is initialized
        redSprite.Hide();
    }

    public void BodyEntered(Node body)
    {
        if (body.IsInGroup("RedBlob"))
        {
            //Set the infected bool to true to change the sprite
            infected = true;
            //Fire the convert event to notify the cell list in main to remove the cell from the unconverted list
            CellConvertEvent ccei = new CellConvertEvent();
            ccei.CovertedCell = this;
            ccei.FireEvent();
            //Swap out sprites to the red converted sprite
            blueSprite.Hide();
            redSprite.Show();
        }
        PlayAudioEvent paei = new PlayAudioEvent();
        paei.soundEffectType = SoundEffectType.HIT;
        paei.AudioTarget = (Node2D)this;
        paei.FireEvent();

    }

    public bool Infected()
    {
        return infected;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
