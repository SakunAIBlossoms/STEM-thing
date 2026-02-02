using Godot;
using System;

public partial class esc_back : Node2D
{
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent &&
			keyEvent.Pressed &&
			keyEvent.Keycode == Key.Escape)
			{
				GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
			}
	}
}
