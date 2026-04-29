using Godot;
using System;

public partial class LightFlicker : SpotLight3D
{
	private Timer IntervalTimer;
	private Timer FlickerTimer;
	private float minInterval = 0.5f;
	private float maxInterval = 4f;
	private float minFlicker = 0.01f;
	private float maxFlicker = 0.4f;
	private float minEnergy = 0f;
	private float maxEnergy = 2f;
	public override void _Ready()
	{
		IntervalTimer = GetNode<Timer>("IntervalTimer");
		FlickerTimer = GetNode<Timer>("FlickerTimer");
		
		IntervalTimer.Timeout += OnIntervalTimerTimeout;
		FlickerTimer.Timeout += OnFlickerTimerTimeout;
	}

	private void OnIntervalTimerTimeout()
	{
		float randomEnergy = (float)GD.RandRange(minEnergy, maxEnergy);
		LightEnergy = randomEnergy;

		float Flicker = (float)GD.RandRange(minFlicker, maxFlicker);
		FlickerTimer.WaitTime = Flicker;
		FlickerTimer.Start();
	}

	private void OnFlickerTimerTimeout()
	{
		LightEnergy = 4;
		float Interval = (float)GD.RandRange(minInterval, maxInterval);
		IntervalTimer.WaitTime = Interval;
		IntervalTimer.Start();
	}

}
