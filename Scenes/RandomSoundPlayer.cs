using System;
using System.Numerics;
using Godot;

public partial class RandomSoundPlayer : Node
{
    [Export]
    public AudioStream[] Sounds;
    private Timer timer;
    private AudioStreamPlayer3D audio;

    private float minDelay = 20f; // seconds
    private float maxDelay = 60f;
    private float minPos = -20f;
    private float maxPos = 20f;

    public override void _Ready()
    {
        GD.Randomize();
        
        timer = GetNode<Timer>("Timer");
        audio = GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");

        timer.Timeout += OnTimerTimeout;

        StartRandomTimer();
    }

    private void OnTimerTimeout()
    {
        MoveRandomLocation();
        PlayRandomSound();
        StartRandomTimer();
    }

    private void PlayRandomSound()
    {
        int index = GD.RandRange(0, Sounds.Length - 1);
        audio.Stream = Sounds[index];
        audio.Play();
    }

    private void MoveRandomLocation()
    {
        float randomPosValue = (float)GD.RandRange(minPos, maxPos)
        Godot.Vector3 randomPos = randomPosValue, randomPosValue, randomPosValue;
        audio.Position = randomPos;
    }
    private void StartRandomTimer()
    {
        float delay = (float)GD.RandRange(minDelay, maxDelay);
        timer.WaitTime = delay;
        timer.Start();
    }
}
