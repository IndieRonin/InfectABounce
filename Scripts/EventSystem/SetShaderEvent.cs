using Godot;
using System;
using EventCallback;
public class SetShaderEvent : Event<SetShaderEvent>
{
    //Disables all the shaders
    public bool disableAll;
    public bool showBlur;
    public bool showBlurAndWater;
    
}
