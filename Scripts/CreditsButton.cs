using Godot;
using System;

public partial class CreditsButton : Button
{
    private AudioStreamPlayer ButtonPressedSound;
    private Timer ButtonTimer;

    public override void _Ready()
    {
        ButtonTimer = GetNode<Timer>("ButtonTimer");
        ButtonTimer.Timeout += OnTimerTimeOut;
        ButtonPressedSound = GetNode<AudioStreamPlayer>("ButtonPressedSound");
        
    }
    public override void _Pressed()
    {
        ButtonPressedSound.Play();
        ButtonTimer.Start();
    }
    public void OnTimerTimeOut()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Credits.tscn");
    }
		
    
}