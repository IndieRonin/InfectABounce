using Godot;
using System;
using System.Collections.Generic;

public class SoundManager : Node2D
{
    AudioStreamPlayer2D soundEffects = new AudioStreamPlayer2D();
    AudioStreamPlayer2D music = new AudioStreamPlayer2D();

List<AudioStreamSample> soundEffectsList = new List<AudioStreamSample>();
List<AudioStreamSample> musicList = new List<AudioStreamSample>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        soundEffects.Name = "SoundEffects";
        AddChild(soundEffects);
        music.Name = "Music";
        AddChild(music);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
