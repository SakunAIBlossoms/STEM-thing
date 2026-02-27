using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class Gameplay : Node
{
	// Setup basic variables, give them a value in _Ready()
	Node2D gui;
	Node3D Env;
	Camera3D Plr;
	AnimationPlayer Animations;
	AudioStreamPlayer MenuMusic;
	AudioStreamPlayer GameMusic;

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

	Vector2 EnvSizeConstraints = new Vector2(10000, 10000);

	Dictionary<string, bool> Hovered = new Dictionary<string, bool> { { "Left", false }, { "Right", false } };

	public override void _Ready()
	{
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
		if (tutorial != null)
		{
			tutorial.QueueFree();
		}
	}
	// Animation hook
	private void AnimationCompleted(StringName name) {
		switch (name)
		{
			case "StartingCutscene":
				gui.GetNode<Panel>("Map").Visible = true;
				gui.GetNode<Control>("Controls").Visible = true;
				gui.GetNode<Control>("Tutorials").Visible = true;
				gui.GetNode<Control>("Tutorials").GetNode<Timer>("TutorialShowLength").Start();
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
	private void DialogPlayerSignal(string signal)
	{
		GD.PrintRich("[color=gold]Gameplay.cs (138) [DialogPlayerSignal] [/color][b]|[/b][color=cyan] Dialog signal ("+signal+") has been triggered");
		switch (signal)
		{
			case "Descend":
				GD.PrintRich("[color=gold]Gameplay.cs (138) [DialogPlayerSignal] [/color][b]|[/b][color=cyan] Begin Descent");
				break;
		}
	}
}
