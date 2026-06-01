using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class LifeSupport : SubViewport
{
	private double oxygenlimit = 1800;
	TimeSpan oxygentimespan;
	private Timer wait;
	private RichTextLabel timeremaining;
	private RichTextLabel oxygenpercent;
	private TextureRect Loading;
	private ProgressBar bar;
	private Timer OxygenLeft;

	private bool CanUpdate = true;

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

	public override void _Process(double delta)
	{
		if (CanUpdate)
		{
			bar.Value = OxygenLeft.TimeLeft;
			oxygentimespan = TimeSpan.FromSeconds(OxygenLeft.TimeLeft);
			timeremaining.Text = oxygentimespan.ToString(@"hh\:mm\:ss");
			oxygenpercent.Text = Mathf.Round(OxygenLeft.TimeLeft / oxygenlimit * 100).ToString() + "%";
		}
	}

	private void PlayerBeenPwned()
	{
		CanUpdate = false;
		timeremaining.Text = "99:99:99";
		oxygenpercent.Text = "999%";
	}

	private void HackFixed()
	{
		CanUpdate = true;
	}
}
