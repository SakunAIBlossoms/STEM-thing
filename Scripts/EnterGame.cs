using Godot;
using System;

public partial class EnterGame : Node2D
{
	private AnimationPlayer _animationplayer;
	private Timer TransitionTimer;
	private AudioStreamPlayer _soundplayer;
	public override void _Ready()
	{
		_animationplayer = GetNode<AnimationPlayer>("FadeToBlack");
		TransitionTimer = GetNode<Timer>("TransitionTimer");
		TransitionTimer.Timeout += OnTimerTimeOut;
		_soundplayer = GetNode<AudioStreamPlayer>("TransitionSound");
	}
	private void OnDialogPlayerDialogEnded()
	{
		_soundplayer.Play();
		TransitionTimer.Start();
	}

	private void OnTimerTimeOut()
	{
		_animationplayer.Play("FadeToBlack");
	}

	private void OnFadeAnimationFinished(StringName animName)
	{
		GetTree().ChangeSceneToFile("res://Scenes/gameplay.tscn");
	}
}
