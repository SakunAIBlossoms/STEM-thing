using Godot;
using System;

public partial class EnterGame : Node2D
{
	private AnimationPlayer _animationplayer;
	private AudioStreamPlayer TransitionSound;
	public override void _Ready()
	{
		_animationplayer = GetNode<AnimationPlayer>("FadeToBlack");

		TransitionSound = GetNode<AudioStreamPlayer>("TransitionSound");

		_animationplayer.AnimationFinished += OnFadeAnimationFinished;
	}
	private void OnDialogPlayerDialogEnded() 
	{
		TransitionSound.Play();
		_animationplayer.Play("FadeToBlack");
	}

	private void OnFadeAnimationFinished(StringName animName)
	{
		GetTree().ChangeSceneToFile("res://Scenes/Gameplay.tscn");
	}
}
