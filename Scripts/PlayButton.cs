using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;

public partial class PlayButton : Button
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
        GetTree().ChangeSceneToFile("res://Scenes/Interlude.tscn");
    }
		
    
}
