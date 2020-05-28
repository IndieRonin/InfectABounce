using Godot;
using System;

public class Main : Node2D
{
    //The scene for the input mananger for the game
    PackedScene inputManagerScene = new PackedScene();
    //The Nodee for the input manager
    Node inputManager;
    //The scene for the UI mananger for the game
    PackedScene uiManagerScene = new PackedScene();
    //The Nodee for the UI manager
    Node uiManager;
    //The scene for the sound mananger for the game
    PackedScene soundManagerScene = new PackedScene();
    //The Nodee for the sound manager
    Node soundManager;
    //The scene for the players actor
    PackedScene redBlobScene = new PackedScene();
    //The player actors
    Node redBlob;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Load all the needed scenes for the games start
        LoadScenes();
        InstatiateScenes();
    }

    private void LoadScenes()
    {
        //Load the scene into the packed scene to load it later
        inputManagerScene = ResourceLoader.Load("res://Scenes/InputManager.tscn") as PackedScene;
        //Load the scene into the packed scene to load it later
        uiManagerScene = ResourceLoader.Load("res://Scenes/UIManager.tscn") as PackedScene;
        //Load the scene into the packed scene to load it later
        soundManagerScene = ResourceLoader.Load("res://Scenes/SoundManager.tscn") as PackedScene;
        //Load the redblob scene
        redBlobScene = ResourceLoader.Load("res://Scenes/RedBlob.tscn") as PackedScene;
    }

    private void InstatiateScenes()
    {
        //Init needed background sytems
        //Instance the input manager and set it as a child of the main scene
        inputManager = inputManagerScene.Instance();
        //Set the input manager as the child of the main scene
        AddChild(inputManager);
        //Instance the UI manager and set it as a child of the main scene
        uiManager = uiManagerScene.Instance();
        //Set the UI manager as the child of the main scene
        AddChild(uiManager);
        //Instance the sound manager and set it as a child of the main scene
        soundManager = soundManagerScene.Instance();
        //Set the sound manager as the child of the main scene
        AddChild(soundManager);

        //Instance thet reb blob scene
        redBlob = redBlobScene.Instance();
        //Set the blobs spawn position
        ((Node2D)redBlob).Position = new Vector2(300, 300);
        //Add the reb blob as a child of the main scene
        AddChild(redBlob);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
