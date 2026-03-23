using Godot;
using System;
using System.IO;
using System.Runtime.Serialization;

public partial class Slider : HSlider
{
	private AudioStreamPlayer SliderSound;
	private double LastSoundTime = 0;
	private const double SoundInterval = 0.1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SliderSound = GetNode("/root/Sounds/Slider") as AudioStreamPlayer;
	}
	public void OnValueChanged(double value)
	{
		double now = Time.GetTicksMsec() / 1000.0;

		if (now - LastSoundTime >= SoundInterval)
		{
			PlayerSliderSound();
			LastSoundTime = now;
		}

	}
	private void PlayerSliderSound()
	{
		SliderSound.Play();
	}

	
}
