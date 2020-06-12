using Godot;
using System;
using System.Collections.Generic;
using EventCallback;

public enum UISoundsType
{
    CLICK,
    TRANSITION
};
public enum SoundEffectType
{
    NONE,
    HIT,
    DEATH
};

public enum MusicType
{
    NONE,
    MENU,
    GAME
};
//Will use this later to help ID the caller for the audio manager, need to optimize the whole soundmanager class :/
public enum AudioCallerID
{
    NONE,
    //Button clicks or meny music
    UI,
    //Hit and death events
    EVENT

};
public class SoundManager : Node2D
{
    //Create a new Audio stream plyerfor the sound effects
    AudioStreamPlayer soundEffects = new AudioStreamPlayer();
    //Create a new audio player for  the music
    AudioStreamPlayer music = new AudioStreamPlayer();
    //The pre loaded sounds for the game kept in a list
    List<AudioStream> soundEffectsList = new List<AudioStream>();
    //The pre loaded music list for the game kept in a list
    List<AudioStream> musicList = new List<AudioStream>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PlayAudioEvent.RegisterListener(PlayAudio);
        //Create the audio stream players and add them as children tot the sound manager
        soundEffects.Name = "SoundEffects";
        soundEffects.VolumeDb = -15f;
        AddChild(soundEffects);
        music.Name = "Music";
        AddChild(music);
        //Load a few audio samples
        soundEffectsList.Add(ResourceLoader.Load("res://Sounds/Effects/impactsplat.wav") as AudioStream);
        soundEffectsList.Add(ResourceLoader.Load("res://Sounds/Effects/ButtonClick.wav") as AudioStream);

        musicList.Add(ResourceLoader.Load("res://Sounds/Music/the_kings_forgotten_medallion.ogg") as AudioStream);

    }

    private void PlayAudio(PlayAudioEvent paei)
    {
        //Change the music type
        if (paei.musicType == MusicType.MENU)
        {
            GD.Print("Play music was called");
            music.Stream = musicList[0];
            music.Play();
        }
        if (paei.uiSoundsType == UISoundsType.CLICK)
        {
            //If the target is not the map it must be one of the blobs so we want to play the impactsplat sound
            soundEffects.Stream = soundEffectsList[1];
            soundEffects.Play();
        }
        if (paei.soundEffectType == SoundEffectType.HIT)
        {
            //Check the target is not the map
            if (!paei.AudioTarget.IsInGroup("Map"))
            {
                //If the target is not the map it must be one of the blobs so we want to play the impactsplat sound
                soundEffects.Stream = soundEffectsList[0];
                soundEffects.Play();
            }
        }
    }



    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
