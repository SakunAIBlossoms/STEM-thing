using Godot;
using System;

public partial class LifeSupport : SubViewport
{

	private double oxygenlimit = 7200;
	TimeSpan oxygentimespan;
	private Timer wait;
	private RichTextLabel timeremaining;
	private RichTextLabel oxygenpercent;
	private TextureRect Loading;
	private ProgressBar bar;
	private Timer OxygenLeft;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		wait = GetNode("wait") as Timer;
		Loading = GetNode("Loading") as TextureRect;
		OxygenLeft = GetNode("OxygenLeft") as Timer;
		timeremaining = GetNode("OxygenTime") as RichTextLabel;
		oxygenpercent = GetNode("OxygenPercentage") as RichTextLabel;
		bar = GetNode("AmountLeft") as ProgressBar;
	}

	private void OnWaitTimeout()
	{
		GetTree().CreateTween().SetTrans(Tween.TransitionType.Circ).SetEase(Tween.EaseType.Out).TweenProperty(Loading, "modulate:a", 0, 1.2f).Finished += () => Loading.Visible = false;
		OxygenLeft.Start();
		wait.QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bar.Value = OxygenLeft.TimeLeft;
		oxygentimespan = TimeSpan.FromSeconds(OxygenLeft.TimeLeft);
		timeremaining.Text = oxygentimespan.ToString(@"hh\:mm\:ss");
		oxygenpercent.Text = (OxygenLeft.TimeLeft / oxygenlimit * 100).ToString() + "%";
	}
}
