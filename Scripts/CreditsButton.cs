using Godot;
using System;

public partial class CreditsButton : Button
{
    private AudioStreamPlayer ButtonPressedSound;

    public override void _Ready()
    {
        ButtonPressedSound = GetNode("/root/Sounds/ButtonPressedSound") as AudioStreamPlayer;
        
    }
    public override void _Pressed()
    {
        ButtonPressedSound.Play();
        GetTree().ChangeSceneToFile("res://Scenes/Credits.tscn");
    }	

}