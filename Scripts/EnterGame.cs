using Godot;
using System;

public partial class EnterGame : Node2D
{
	private AnimationPlayer _animationplayer;
	private AudioStreamPlayer _soundplayer;
	public override void _Ready()
	{
		_animationplayer = GetNode<AnimationPlayer>("FadeToBlack");

		_soundplayer = GetNode<AudioStreamPlayer>("TransitionSound");
	}
	private void OnDialogPlayerDialogEnded() 
	{
		_soundplayer.Play();
		_animationplayer.Play("FadeToBlack");
	}

	private void OnFadeAnimationFinished(StringName animName)
	{
		GD.Print("Animation " + (string)animName + " is finished.");
		GetTree().ChangeSceneToFile("res://Scenes/Gameplay.tscn");
	}
}
