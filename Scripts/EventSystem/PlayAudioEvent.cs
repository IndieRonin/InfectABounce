using Godot;
using System;

namespace EventCallback
{
    public class PlayAudioEvent : Event<PlayAudioEvent>
    {
        //The object the sound effect is for, eg. death of a player or a hit for the player
        public Node AudioTarget;
        public UISoundsType uiSoundsType;
        public SoundEffectType soundEffectType;
        public MusicType musicType;
    }
}
