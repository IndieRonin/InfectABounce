using Godot;
using System;
using EventCallback;
public class Main : Node2D
{
    //The scene for the input mananger for the game
    PackedScene inputManagerScene = new PackedScene();
    //The Nodee for the input manager
    Node inputManager;
    //The scene for the UI mananger for the game
    PackedScene uiManagerScene = new PackedScene();
    //The Node for the UI manager
    Node uiManager;
    //The scene for the sound mananger for the game
    PackedScene soundManagerScene = new PackedScene();
    //The Node for the sound manager
    Node soundManager;

    //The scene for the sound mananger for the game
    PackedScene mapScene = new PackedScene();
    //The Node for the sound manager
    Node map;
    //The scene for the camera scene for the game
    PackedScene cameraScene = new PackedScene();
    //The Node for the camera manager
    Node camera;

    //The scene for the players actor
    PackedScene redBlobScene = new PackedScene();
    //The player actors
    Node redBlob;
    //The scene for the players actor
    PackedScene blueBlobScene = new PackedScene();
    //The player actors
    Node blueBlob;
    //The scene for the players actor
    PackedScene greenBlobScene = new PackedScene();
    //The player actors
    Node greenBlob;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        UIInputEvent.RegisterListener(GetUIInput);
        //Load all the needed scenes for the games start
        LoadScenes();
        InstatiateScenes();
    }

    private void LoadScenes()
    {
        //Load the input manager scene into the packed scene to load it later
        inputManagerScene = ResourceLoader.Load("res://Scenes/InputManager.tscn") as PackedScene;
        //Load the UI manager scene into the packed scene to load it later
        uiManagerScene = ResourceLoader.Load("res://Scenes/UIManager.tscn") as PackedScene;
        //Load the sound manager scene into the packed scene to load it later
        soundManagerScene = ResourceLoader.Load("res://Scenes/SoundManager.tscn") as PackedScene;

        //Load the map scene into the packed scene to load it later
        mapScene = ResourceLoader.Load("res://Scenes/Map.tscn") as PackedScene;

        //Load the map scene into the packed scene to load it later
        cameraScene = ResourceLoader.Load("res://Scenes/Camera.tscn") as PackedScene;

        //Load the redblob scene
        redBlobScene = ResourceLoader.Load("res://Scenes/RedBlob.tscn") as PackedScene;
        //Load the redblob scene
        blueBlobScene = ResourceLoader.Load("res://Scenes/BlueBlob.tscn") as PackedScene;
        //Load the redblob scene
        greenBlobScene = ResourceLoader.Load("res://Scenes/GreenBlob.tscn") as PackedScene;
    }

    private void InstatiateScenes()
    {
        //Init needed background sytems
        //Instance the input manager and set it as a child of the main scene
        inputManager = inputManagerScene.Instance();
        //Set the input manager as the child of the main scene
        AddChild(inputManager);
        //Instance the map and set it as a child of the main scene
        camera = cameraScene.Instance();
        //Set the map as the child of the main scene
        AddChild(camera);
        //Instance the UI manager and set it as a child of the main scene
        uiManager = uiManagerScene.Instance();
        //Set the UI manager as the child of the main scene
        AddChild(uiManager);
        //Instance the sound manager and set it as a child of the main scene
        soundManager = soundManagerScene.Instance();
        //Set the sound manager as the child of the main scene
        AddChild(soundManager);
    }

    private void StartGame()
    {
        GD.Print("StartGame");
        //Instance the map and set it as a child of the main scene
        map = mapScene.Instance();
        //Set the map as the child of the main scene
        AddChild(map);

        //Instance thet reb blob scene
        redBlob = redBlobScene.Instance();
        //Set the blobs spawn position
        ((Node2D)redBlob).Position = new Vector2(300, 300);
        //Add the reb blob as a child of the main scene
        AddChild(redBlob);

        //Instance thet reb blob scene
        blueBlob = blueBlobScene.Instance();
        //Set the blobs spawn position
        ((Node2D)blueBlob).Position = new Vector2(500, 300);
        //Add the reb blob as a child of the main scene
        AddChild(blueBlob);

        //Instance thet reb blob scene
        greenBlob = greenBlobScene.Instance();
        //Set the blobs spawn position
        ((Node2D)greenBlob).Position = new Vector2(700, 300);
        //Add the reb blob as a child of the main scene
        AddChild(greenBlob);
    }

    private void GetUIInput(UIInputEvent uiiei)
    {
        if (uiiei.startPressed) StartGame();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
