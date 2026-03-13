using System;
using Godot;

public partial class RandomSoundPlayer : Node
{
    [Export]
    public AudioStream[] Sounds;
    private Timer timer;
    private AudioStreamPlayer audio;

    private float minDelay = 20f; // seconds
    private float maxDelay = 60f;

    public override void _Ready()
    {
        GD.Randomize();

        timer = GetNode<Timer>("Timer");
        audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

        timer.Timeout += OnTimerTimeout;

        StartRandomTimer();
    }

    private void OnTimerTimeout()
    {
        PlayRandomSound();
        StartRandomTimer();
    }

    private void PlayRandomSound()
    {
        int index = GD.RandRange(0, Sounds.Length - 1);
        audio.Stream = Sounds[index];
        audio.Play();
    }
    private void StartRandomTimer()
    {
        float delay = (float)GD.RandRange(minDelay, maxDelay);
        timer.WaitTime = delay;
        timer.Start();
    }
}
