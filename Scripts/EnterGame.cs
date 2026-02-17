using Godot;
using System;

public partial class EnterGame : Node2D
{
	private AnimationPlayer _animationplayer;
	
	public override void _Ready()
	{
		_animationplayer = GetNode<AnimationPlayer>("FadeToBlack");

		_animationplayer.AnimationFinished += OnFadeAnimationFinished;
	}
	private void OnDialogPlayerDialogEnded() 
	{
		_animationplayer.Play("FadeToBlack");
	}

	private void OnFadeAnimationFinished(FadeToBlack)
	{
		GetTree().ChangeSceneToFile("res://Scenes/Gameplay.tscn");
	}
}
