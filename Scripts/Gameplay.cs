using Godot;
using System;

public partial class Gameplay : Node
{

	// Setup basic variables, give them a value in _Ready()
	Node2D gui;
	Node3D Env;
	Camera3D Plr;

	public override void _Ready()
	{
		// Give variables a value first
		gui = GetNode("GUI") as Node2D;
		Env = GetNode("Environment") as Node3D;
		Plr = Env.GetNode("Player") as Camera3D;
		// Then anything else important later
		if (!Plr.Current) Plr.Current = true;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
