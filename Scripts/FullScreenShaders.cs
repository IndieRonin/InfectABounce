using Godot;
using System;
using EventCallback;

public class FullScreenShaders : Node2D
{

    //The color rectangle that has the shader on that we want to pass the variables to
    ColorRect crossHairShader;
    //The blur shader for the scene
    ColorRect blurShader;
//The water shader for the scene
    ColorRect blurAndWaterShader;

Node2D target = null;

    public override void _Ready()
    {
        SetShaderEvent.RegisterListener(SetShader);

        crossHairShader = GetNode<ColorRect>("CanvasLayer/CrossHair");
        blurShader = GetNode<ColorRect>("CanvasLayer/Blur");
        blurAndWaterShader = GetNode<ColorRect>("CanvasLayer/BlurAndWater");
        
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //if(target == null) return;

        Vector2 mousePos = GetViewport().GetMousePosition();
        mousePos.x /= GetViewportRect().Size.x;
        mousePos.y /= GetViewportRect().Size.y;
        (blurAndWaterShader.Material as ShaderMaterial).SetShaderParam("target", mousePos);

        //(crossHairShader.Material as ShaderMaterial).SetShaderParam("target", target.Position);
        //(blurShader.Material as ShaderMaterial).SetShaderParam("target", target.Position);
    }

    private void DisableAllShaders()
{
    crossHairShader.Hide();
    blurShader.Hide();
}

    private void SetShader(SetShaderEvent ssei)
    {
        //To be done
        //if(ssei.disableAll);

    }
}
