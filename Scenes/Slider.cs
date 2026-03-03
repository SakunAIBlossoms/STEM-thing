using Godot;
using System;
using System.IO;
using System.Runtime.Serialization;

public partial class Slider : HSlider
{
	private AudioStreamPlayer SliderSound;
	private Timer DebounceTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SliderSound = GetNode("/root/Sounds/SliderSound") as AudioStreamPlayer;
		DebounceTimer = GetNode("DebounceTimer") as Timer;
		DebounceTimer.Timeout += OnTimerTimeout;
	}
	public void OnValueChanged(double value)
	{
		DebounceTimer.Start();
	}

	public void OnTimerTimeout()
	{
		SliderSound.Play();
	}

}
