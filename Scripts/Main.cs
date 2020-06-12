using Godot;
using System;
using System.Collections.Generic;
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
    //The scene for the UI mananger for the game
    PackedScene fullscreenShaderScene = new PackedScene();
    //The Node for the UI manager
    Node fullscreenShader;
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

    //The amount of cels left to convert before victory is achieved
    List<Node> cellsLeftToConvert = new List<Node>();

    //The amount of inti body cells
    List<Node> greenBlobList = new List<Node>();
    //List to keep track of all the blue blobs to free theem when the game eneds
    List<Node> blueBlobsList = new List<Node>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        UIInputEvent.RegisterListener(GetUIInput);
        CellConvertEvent.RegisterListener(CellConverted);
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
        //Load the UI manager scene into the packed scene to load it later
        fullscreenShaderScene = ResourceLoader.Load("res://Scenes/FullScreenShaders.tscn") as PackedScene;
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

        //Instance the UI manager and set it as a child of the main scene
        uiManager = uiManagerScene.Instance();
        //Set the UI manager as the child of the main scene
        AddChild(uiManager);
        //Instance the sound manager and set it as a child of the main scene
        soundManager = soundManagerScene.Instance();
        //Set the sound manager as the child of the main scene
        AddChild(soundManager);
        //Instance the fullscreenshader and set it as a child of the main scene
        fullscreenShader = fullscreenShaderScene.Instance();
        //Set the map as the child of the main scene
        AddChild(fullscreenShader);
        SetShaderEvent ssei = new SetShaderEvent();
        ssei.showBlurAndWater = true;
        ssei.FireEvent();
    }

    private void StartGame()
    {
        SetShaderEvent ssei = new SetShaderEvent();
        ssei.showBlur = true;
        ssei.FireEvent();
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
        //Set the camera as the child of the main scene
        AddChild(camera);

        CameraEvent cei = new CameraEvent();
        cei.target = (Node2D)redBlob;
        cei.dragMarginHorizontal = true;
        cei.dragMarginVertical = true;
        cei.FireEvent();

        SpawnGreenBlobs();
        SpawnBlueBlobs();
    }

    private void StopGame()
    {
        map.QueueFree();
        //Change the shader for the end game screens
        SetShaderEvent ssei = new SetShaderEvent();
        ssei.showBlurAndWater = true;
        ssei.FireEvent();
        //Set the camera back to the main scene
        CameraEvent cei = new CameraEvent();
        cei.target = (Node2D)redBlob.GetParent();
        cei.dragMarginHorizontal = false;
        cei.dragMarginVertical = false;
        cei.FireEvent();

        blueBlob.QueueFree();

        redBlob.QueueFree();

        greenBlob.QueueFree();
    }

    private void PlayerDied(DeathEvent dei)
    {
        if (dei.target.IsInGroup("RedBlob"))
        {
            GameOverEvent goei = new GameOverEvent();
            goei.FireEvent();
            StopGame();
        }
    }

    private void CellConverted(CellConvertEvent ccei)
    {
        for (int i = 0; i < cellsLeftToConvert.Count; i++)
        {
            if (cellsLeftToConvert[i].GetInstanceId() == ccei.CovertedCell.GetInstanceId())
            {
                cellsLeftToConvert.RemoveAt(i);
                if (cellsLeftToConvert.Count == 0)
                {
                    StopGame();
                    WinEvent wei = new WinEvent();
                    wei.FireEvent();
                }
            }
        }
    }

    private void GetUIInput(UIInputEvent uiiei)
    {
        if (uiiei.startPressed) StartGame();
    }

    private void SpawnGreenBlobs()
    {
        Vector2 newSpawnPoint = Vector2.Zero;
        Vector2 lastSpawnPoint = Vector2.Zero;
        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();

        //The minimum and maximum spawn distance from the last spawned position
        int minSpawnDistance = 400, maxSpawnDistance = 800;

        for (int y = 0; y < 2; y++)
        {
            newSpawnPoint.y = rng.RandiRange(minSpawnDistance, maxSpawnDistance) + lastSpawnPoint.y;
            lastSpawnPoint.x = 0f;
            for (int x = 0; x < 4; x++)
            {
                newSpawnPoint.x = rng.RandiRange(minSpawnDistance, maxSpawnDistance) + lastSpawnPoint.x;

                greenBlob = greenBlobScene.Instance();
                ((Node2D)greenBlob).Position = newSpawnPoint;
                ((RigidBody2D)greenBlob).LinearVelocity = new Vector2(rng.Randf() * 30, rng.Randf() * 30);
                lastSpawnPoint = newSpawnPoint;
                AddChild(greenBlob);
                greenBlobList.Add(greenBlob);

                rng.Randomize();
            }
        }
    }

    private void SpawnBlueBlobs()
    {
        Vector2 newSpawnPoint = Vector2.Zero;
        Vector2 lastSpawnPoint = Vector2.Zero;
        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();
        //The minimum and maximum spawn distance from the last spawned position
        int minSpawnDistance = 300, maxSpawnDistance = 500;
        for (int y = 0; y < 4; y++)
        {
            newSpawnPoint.y = rng.RandiRange(minSpawnDistance, maxSpawnDistance) + lastSpawnPoint.y;
            lastSpawnPoint.x = 0f;
            for (int x = 0; x < 7; x++)
            {
                newSpawnPoint.x = rng.RandiRange(minSpawnDistance, maxSpawnDistance) + lastSpawnPoint.x;

                blueBlob = blueBlobScene.Instance();
                ((Node2D)blueBlob).Position = newSpawnPoint;
                ((RigidBody2D)blueBlob).LinearVelocity = new Vector2(rng.Randf() * 30, rng.Randf() * 30);
                lastSpawnPoint = newSpawnPoint;
                AddChild(blueBlob);
                cellsLeftToConvert.Add(blueBlob);
                blueBlobsList.Add(blueBlob);
                rng.Randomize();
            }
        }
    }
}
