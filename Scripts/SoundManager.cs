using Godot;
using System;
using System.Collections.Generic;

public class SoundManager : Node2D
{
    AudioStreamPlayer2D soundEffects = new AudioStreamPlayer2D();
    AudioStreamPlayer2D music = new AudioStreamPlayer2D();

List<AudioStream> soundEffectsList = new List<AudioStream>();
List<AudioStream> musicList = new List<AudioStream>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        soundEffects.Name = "SoundEffects";
        AddChild(soundEffects);
        music.Name = "Music";
        AddChild(music);

        soundEffectsList.Add(ResourceLoader.Load("res://Sounds/Effects/impactsplat.wav") as AudioStream);
        musicList.Add(ResourceLoader.Load("res://Sounds/Music/the_kings_forgotten_medallion.ogg") as AudioStream);

        soundEffects.Stream = soundEffectsList[0];
        soundEffects.Play();

        music.Stream = musicList[0];
        music.Play();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
