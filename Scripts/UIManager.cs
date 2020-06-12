using Godot;
using System;
using EventCallback;

public class UIManager : Control
{
    //Grab the screen nodes in the UI
    VBoxContainer menu;
    VBoxContainer win;
    VBoxContainer gameOver;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        WinEvent.RegisterListener(ShowWin);
        GameOverEvent.RegisterListener(ShowGameOver);
        menu = GetNode<VBoxContainer>("Menu");
        win = GetNode<VBoxContainer>("Win");
        gameOver = GetNode<VBoxContainer>("GameOver");

        ShowMenu();
    }

    private void HideAll()
    {
        menu.Hide();
        win.Hide();
        gameOver.Hide();
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

    private void ShowWin(WinEvent wei)
    {
        HideAll();
        win.Show();
    }

    private void ShowGameOver(GameOverEvent goei)
    {
         HideAll();
        gameOver.Show();
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
