using System;
using System.Numerics;
using Godot;

public partial class RandomSoundPlayer : Node
{
    [Export]
    public AudioStream[] Sounds;
    private Timer timer;
    private AudioStreamPlayer3D audio;

    private int minDelay = 20; // seconds
    private int maxDelay = 60;
    private int minPos = -20;
    private int maxPos = 20;
    private float insideSubX = 1.7f; -1.8f
    private float insideSubY = -1.7f; -0.4f
    private float insideSubZ = 2.6f; -1.9f

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
        int randomX = (int)GD.RandRange(minPos, maxPos);
        int randomY = (int)GD.RandRange(minPos, maxPos);
        int randomZ = (int)GD.RandRange(minPos, maxPos);
        Godot.Vector3 randomPos = new Godot.Vector3(randomX, randomY, randomZ);
        audio.Position = randomPos;
        if (audio.Position.X <= insideSub)
        {
            MoveRandomLocation();
        }
        GD.Print("Sound position is ", randomPos);
    }
    private void StartRandomTimer()
    {
        int delay = (int)GD.RandRange(minDelay, maxDelay);
        timer.WaitTime = delay;
        timer.Start();
        GD.Print("Next sound in ", delay, " seconds");
    }
}
