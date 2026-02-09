using Godot;
using System;

public partial class Gameplay : Node
{
	// Setup basic variables, give them a value in _Ready()
	Node2D gui;
	Node3D Env;
	Camera3D Plr;
	AnimationPlayer Animations;

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

	bool MapOpen = false;

	public override void _Ready()
	{
		// Give variables a value first
		gui = GetNode("GUI") as Node2D;
		Env = GetNode("Environment") as Node3D;
		Plr = Env.GetNode("Player") as Camera3D;
		Animations = GetNode("Animations") as AnimationPlayer;
		// Then anything else important later
		if (!Plr.Current) Plr.Current = true;
		
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
			OS.Alert(e.Message+"\n\nEXITING...", "ERROR CAUGHT");
			GetTree().Quit((int) Error.DoesNotExist);
		}
		if (tutorial != null)
		{
			tutorial.QueueFree();
		}
	}

	// Camera Controls
	private void LeftSideEntered()
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
	private void RightSideEntered()
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
	private void CheckMap()
	{
		GD.Print("Open Map");
	}
}