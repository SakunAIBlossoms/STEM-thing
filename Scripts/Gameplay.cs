using Godot;
using System;
using System.Collections.Generic;

public partial class Gameplay : Node
{
    // Setup basic variables, give them a value in _Ready()
    private Node2D gui;
    private Node3D Env;
    private Camera3D Plr;
    private AnimationPlayer Animations;
    private AudioStreamPlayer MenuMusic;
    private AudioStreamPlayer GameMusic;
    private TextureRect HACKEDUI;
    private Button ResetButton;

    // Map related variables
    private Vector2I EnvironmentSize = new Vector2I(50, 50);
    private Vector2 GridSlotSize = new Vector2();
    private Vector2I SubPosition = new Vector2I();

	private int ObstacleCountToSpawn = 10;
    private int ObjectCountToSpawn = 8;
    private double MovementTime = 2.2;

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

    // who hacked me bruh im crine
    public bool HACKED {get; set;} = true;

    public override void _Ready()
    {
        // what the fuck is this spaghetti, and why is it so long
        GridSlotSize = new Vector2(((int)ProjectSettings.GetSetting("display/window/size/viewport_width") / EnvironmentSize.X), ((int)ProjectSettings.GetSetting("display/window/size/viewport_height") / EnvironmentSize.Y));
        SubPosition = new Vector2I(EnvironmentSize.X / 2, EnvironmentSize.Y / 2); // This should hopefully center the player

        // Give variables a value first
        gui = GetNode("GUI") as Node2D;
        Env = GetNode("Environment") as Node3D;
        Plr = Env.GetNode("Player") as Camera3D;
        Animations = GetNode("Animations") as AnimationPlayer;
        HACKEDUI = GetNode("PWNED/PWNED") as TextureRect;
        ResetButton = GetNode("GUI/RESET") as Button;
        GD.PrintRich("Line 38 Gameplay.cs: animation is null? " + (Animations == null));

        // Then anything else important later
        if (!Plr.Current) Plr.Current = true;
        Animations.AnimationFinished += AnimationCompleted;

        // Stop the menu music and play the music for when you're actually in-game
        MenuMusic = GetNode("/root/MenuMusic") as AudioStreamPlayer;
        MenuMusic.Stop();
        GameMusic = GetNode<AudioStreamPlayer>("GameMusic");
        GameMusic.Play();
    }

    public override void _Process(double delta)
    {
        HACKEDUI.Visible = HACKED;
        ResetButton.Visible = HACKED;
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

    public override void _Input(InputEvent @event)
    {
        if (!HACKED) MoveDirection(@event.AsText());
        base._Input(@event);
        //if (@event.AsText() == "")
    }

    private void MoveDirection(String direction, int amount = 1)
    {
        switch (direction)
        {
            case "W":
                SubPosition.Y += amount;
                break;
            case "S":
                SubPosition.Y -= amount;
                break;
            case "A":
                SubPosition.X -= amount;
                break;
            case "D":
                SubPosition.X += amount;
                break;
            default:
                GD.PrintRaw("Cannot move, key [color=cyan]" + direction + " is not valid!");
                break;
        }
        GD.PrintRaw("Move Sub Key [color=cyan]"+direction+"[/color] Pressed, moved that direction by [color=cyan]"+amount.ToString());
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
    private void ResetButtonPressed()
    {
        HACKED = false;
    }
    private void HackedChanceTrigger()
    {
        var rng = new Random();
        var num = rng.Next(0, 1);
        if (num == 0)
        {
            HACKED = true;
        }
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
