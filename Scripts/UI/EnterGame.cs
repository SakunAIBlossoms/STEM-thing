using Godot;
using System;

public partial class EnterGame : Node2D
{
	private AnimationPlayer _animationplayer;
	private Timer TransitionTimer;
	private AudioStreamPlayer TransitionSound;
	private ColorRect OverlayFade;
	private AudioStreamPlayer MenuMusic;
	private AudioStreamPlayer InterludeMusic;
	public override void _Ready()
	{
		InterludeMusic = GetNode("InterludeMusic") as AudioStreamPlayer;
		MenuMusic = GetNode("/root/MenuMusic") as AudioStreamPlayer;
		MenuMusic.Stop();
		OverlayFade = GetNode("/root/Overlay/Fade") as ColorRect;
		_animationplayer = GetNode<AnimationPlayer>("FadeToBlack");
		TransitionTimer = GetNode<Timer>("TransitionTimer");
		TransitionTimer.Timeout += OnTimerTimeOut;
		TransitionSound = GetNode<AudioStreamPlayer>("TransitionSound");
		if (OverlayFade != null)
		{
			Tween twn = GetTree().CreateTween();
			twn.TweenProperty(OverlayFade, "color:a", 0, 0.8);
		}
	}
	private void OnDialogPlayerDialogEnded()
	{
		InterludeMusic.Stop();
		TransitionSound.Play();
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
