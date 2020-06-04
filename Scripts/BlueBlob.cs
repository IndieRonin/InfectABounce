using Godot;
using System;

public class BlueBlob : RigidBody2D
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
            infected = true;
            blueSprite.Hide();
            redSprite.Show();
        }
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
