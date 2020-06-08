using Godot;
using System;
using EventCallback;

public class UIManager : Control
{
    //Grab the screen nodes in the UI
    VBoxContainer menu;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        menu = GetNode<VBoxContainer>("Menu");

        ShowMenu();
    }

    private void HideAll()
    {
        menu.Hide();
    }

    private void ShowMenu()
    {
        PlayAudioEvent paei = new PlayAudioEvent();
        paei.musicType = MusicType.MENU;
        paei.FireEvent();

        menu.Show();
    }

    private void ShowHUD()
    {
        HideAll();

    }

    private void ShowWin()
    {

    }

    private void ShowGameOver()
    {


    }
    public void StartPressed()
    {
        UIInputEvent uiiei = new UIInputEvent();
        uiiei.startPressed = true;
        uiiei.FireEvent();

        PressSound();
        ShowHUD();
    }

    public void ExitPressed()
    {
        PressSound();
        GetTree().Quit();
    }

    private void PressSound()
    {
        PlayAudioEvent paei = new PlayAudioEvent();
        paei.uiSoundsType = UISoundsType.CLICK;
        paei.FireEvent();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
