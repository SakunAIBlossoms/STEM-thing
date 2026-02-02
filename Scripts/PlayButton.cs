using Godot;
using System;

public partial class PlayButton : Button
{
	public override void _Pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Interlude.tscn");
	}
}
