using Godot;
using System;

public partial class EnterGame : Node2D
{
	private AnimationPlayer _animationplayer;
	private AudioStreamPlayer TransitionSound;
	public override void _Ready()
	{
		_animationplayer = GetNode<AnimationPlayer>("FadeToBlack");
	}
	private void OnDialogPlayerDialogEnded() 
	{
		GD.Print("Playing animation " + "FadeToBlack");
		_animationplayer.Play("FadeToBlack");
	}

	private void OnFadeAnimationFinished(StringName animName)
	{
		GD.Print("Animation " + (string)animName + " is finished.");
		GetTree().ChangeSceneToFile("res://Scenes/Gameplay.tscn");
	}
}
