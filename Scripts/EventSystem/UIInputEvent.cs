using Godot;
using System;

namespace EventCallback
{
    public class UIInputEvent : Event<UIInputEvent>
    {
        public bool startPressed;
    }
}