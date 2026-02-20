using Godot;
using System;

public partial class CreditsButton : Button
{
	public override void _Pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Credits.tscn");
	}
}
