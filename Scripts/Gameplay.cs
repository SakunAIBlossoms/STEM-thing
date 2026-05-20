using Godot;
using System;
using System.Collections.Generic;

public partial class Gameplay : Node
{
    Vector2I EnvironmentSize = new Vector2I(50, 50);
    Vector2 GridSlotSize = new Vector2(0,0);

    // Setup basic variables, give them a value in _Ready()
    Node2D gui;
    Node3D Env;
    Camera3D Plr;
    AnimationPlayer Animations;
    AudioStreamPlayer MenuMusic;
    AudioStreamPlayer GameMusic;

	int ObstacleCountToSpawn = 10;
    int ObjectCountToSpawn = 8;
    double MovementTime = 2.2;

    Dictionary<string, Variant> ScannableObjects = new Dictionary<string, Variant> { };
    Dictionary<int, Vector2> ObstacleObjects = new Dictionary<int, Vector2> { };

    // Custom variables WE make twin
    // Make some camera stuff, we need to track the direction and i feel like an enum is the best way to do this
    enum CameraFocus
    {
        Left,
        Front,
        Right,
        Map
    }
    CameraFocus CurrentCamDirection = CameraFocus.Front;

    bool Cutscene = true;

    Dictionary<string, bool> Hovered = new Dictionary<string, bool> { { "Left", false }, { "Right", false } };

    public override void _Ready()
    {
        GridSlotSize = new Vector2(((int)ProjectSettings.GetSetting("display/window/size/viewport_width") / EnvironmentSize.X), ((int)ProjectSettings.GetSetting("display/window/size/viewport_height") / EnvironmentSize.Y));
        MenuMusic = GetNode("/root/MenuMusic") as AudioStreamPlayer;
        MenuMusic.Stop();
        GameMusic = GetNode<AudioStreamPlayer>("GameMusic");
        GameMusic.Play();
        // Give variables a value first
        gui = GetNode("GUI") as Node2D;
        Env = GetNode("Environment") as Node3D;
        Plr = Env.GetNode("Player") as Camera3D;
        Animations = GetNode("Animations") as AnimationPlayer;
        GD.PrintRich("Line 38 Gameplay.cs: animation is null? " + (Animations == null));
        // Then anything else important later
        if (!Plr.Current) Plr.Current = true;
        Animations.AnimationFinished += AnimationCompleted;
	
    }

    // Randomly generates things for the player to find and obstacles that hitting causes the player to take damage.
    private void GenerateEnvironment()
    {
        GD.PrintRich("[color=gold]GENERATING ENVIRONMENT...");
        for (int i = 0; i < ObstacleCountToSpawn; i++)
        {
            var Position = GetRandomPos();
            if (ObstacleObjects.Count > 0)
            {
                foreach (var data in ObstacleObjects)
                {
                    if (data.Value == Position)
                    {
                        Position = GetRandomPos();
                        GD.Print(Position);
                    }
                }
            }
            ObstacleObjects.Add(i, Position);
        }
    }

    private Vector2 GetRandomPos() {
        Random rnd = new Random();
        var x = rnd.Next(0, EnvironmentSize.X);
        var y = rnd.Next(0, EnvironmentSize.Y);
        return new Vector2(x, y);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        //if (@event.AsText() == "")
    }

    // Any Events we need to execute
    private void OnTutorialShowLengthTimeout()
    {
        Control tutorial = null;
        try
        {
            tutorial = gui.GetNode("Tutorials") as Control;
        }
        catch (Exception e)
        {
            OS.Alert(e.Message + "\n\nEXITING...", "ERROR CAUGHT");
            GetTree().Quit((int)Error.DoesNotExist);
        }
        tutorial?.QueueFree();
    }
    // Animation hook
    private void AnimationCompleted(StringName name)
    {
        switch (name)
        {
            case "StartingCutscene":
                gui.GetNode<Panel>("Map").Visible = true;
                gui.GetNode<Control>("Controls").Visible = true;
                gui.GetNode<Control>("Tutorials").Visible = true;
                gui.GetNode<Control>("Tutorials").GetNode<Timer>("TutorialShowLength").Start();
                GetNode<SubViewport>("ShipStats").GetNode<Timer>("Timer").Start();
                GetNode<SubViewport>("LifeSupport").GetNode<Timer>("wait").Start();
                Animations.Play("RESET");
                break;
            case "LookFrontFromLeft":
                if (Hovered["Right"]) MoveRight();
                break;
            case "LookFrontFromRight":
                if (Hovered["Left"]) MoveLeft();
                break;
        }
        return;
    }


    // Camera Controls
    private void LeftSideEntered()
    {
        Hovered["Left"] = true;
        MoveLeft();
    }
    private void LeftSideExited()
    {
        Hovered["Left"] = false;
    }
    private void RightSideEntered()
    {
        Hovered["Right"] = true;
        MoveRight();
    }
    private void RightSideExited()
    {
        Hovered["Right"] = false;
    }
    private void MoveRight()
    {
        if (CurrentCamDirection != CameraFocus.Right)
        {
            switch (CurrentCamDirection)
            {
                case CameraFocus.Left:
                    CurrentCamDirection = CameraFocus.Front;
                    Animations.Play("LookFrontFromLeft");
                    break;
                case CameraFocus.Front:
                    CurrentCamDirection = CameraFocus.Right;
                    Animations.Play("LookRight");
                    break;
            }
        }
    }
    private void MoveLeft()
    {
        if (CurrentCamDirection != CameraFocus.Left)
        {
            switch (CurrentCamDirection)
            {
                case CameraFocus.Right:
                    CurrentCamDirection = CameraFocus.Front;
                    Animations.Play("LookFrontFromRight");
                    break;
                case CameraFocus.Front:
                    CurrentCamDirection = CameraFocus.Left;
                    Animations.Play("LookLeft");
                    break;
            }
        }
    }
    private void CheckMap()
    {
        GD.Print("Check Map");
        if (CurrentCamDirection != CameraFocus.Map)
        {
            switch (CurrentCamDirection)
            {
                case CameraFocus.Left:
                    MoveRight();
                    CurrentCamDirection = CameraFocus.Map;
                    Animations.Play("CheckMap");
                    break;
                case CameraFocus.Right:
                    MoveLeft();
                    CurrentCamDirection = CameraFocus.Map;
                    Animations.Play("CheckMap");
                    break;
                case CameraFocus.Front:
                    CurrentCamDirection = CameraFocus.Map;
                    Animations.Play("CheckMap");
                    break;
            }
        }
        else
        {
            CurrentCamDirection = CameraFocus.Front;
            Animations.Play("LookUpFromMap");
            GD.Print("Look Up");
        }
    }
}
