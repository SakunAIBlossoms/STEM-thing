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
	private RichTextLabel InterludeText;
	private RichTextLabel ClickToContinue;
	private RichTextLabel LocatingDestination;
	private Timer DotDelay;
	private Timer FlashingText;
	public override void _Ready()
	{
		InterludeMusic = GetNode("InterludeMusic") as AudioStreamPlayer;
		MenuMusic = GetNode("/root/MenuMusic") as AudioStreamPlayer;
		MenuMusic.Stop();
		OverlayFade = GetNode("/root/Overlay/Fade") as ColorRect;
		TransitionTimer = GetNode<Timer>("TransitionTimer");
		TransitionTimer.Timeout += OnTimerTimeOut;
		TransitionSound = GetNode<AudioStreamPlayer>("TransitionSound");
		InterludeText = GetNode<RichTextLabel>("CRT/InterludeText");
		ClickToContinue = GetNode<RichTextLabel>("CRT/ClickToContinue");
		LocatingDestination = GetNode<RichTextLabel>("LocatingDestination");
		DotDelay = LocatingDestination.GetNode<Timer>("DotDelay");
		FlashingText = ClickToContinue.GetNode<Timer>("FlashingText");

		if (OverlayFade != null)
		{
			Tween twn = GetTree().CreateTween();
			twn.TweenProperty(OverlayFade, "color:a", 0, 0.8);
		}
	}

	private void OnClick()
	{
		InterludeMusic.Stop();
		TransitionSound.Play();
		TransitionTimer.Start();
		Tween twn = GetTree().CreateTween();
		twn.TweenProperty(OverlayFade, "color:a", 1, 3);
		DotDelay.Stop();
		FlashingText.Stop();
		LocatingDestination.Text = "     Location Found";
		ClickToContinue.Text = " ";
		InterludeText.Text = " ";
	}
         
 	private void OnTimerTimeOut()
	{
		GetTree().ChangeSceneToFile("res://Scenes/gameplay.tscn");
	}
}
