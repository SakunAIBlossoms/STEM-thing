using Godot;
using System;

public partial class EnterGame : Node2D
{
	private void OndialogPlayerDialogEnded() 
	{
		GetTree().ChangeSceneToFile("res://Scenes/Gameplay.tscn");
	}
}
