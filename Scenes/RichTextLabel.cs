using Godot;
using System;

public partial class LocatingDestination : Godot.RichTextLabel
{
	private Timer Interval;
	public override void _Ready()
	{
		Interval = GetNode<Timer>("IntervalTimer");
	}

	private void OnTimerTimeout()
	{

	}
}
