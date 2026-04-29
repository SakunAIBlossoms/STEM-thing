using Godot;
using System;

public partial class EnterGame : Node2D
{
	private AnimationPlayer _animationplayer;
	private Timer TransitionTimer;
	private AudioStreamPlayer _soundplayer;
	private ColorRect OverlayFade;
	public override void _Ready()
	{
		OverlayFade = GetNode("/root/Overlay/Fade") as ColorRect;
		_animationplayer = GetNode<AnimationPlayer>("FadeToBlack");
		TransitionTimer = GetNode<Timer>("TransitionTimer");
		TransitionTimer.Timeout += OnTimerTimeOut;
		_soundplayer = GetNode<AudioStreamPlayer>("TransitionSound");
		if (OverlayFade != null)
		{
			Tween twn = GetTree().CreateTween();
			twn.TweenProperty(OverlayFade, "color:a", 0, 0.8);
		}
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
