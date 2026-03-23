using Godot;
using System;

public partial class BackButton : Button
{
    private AudioStreamPlayer ButtonPressedSound;

    public override void _Ready()
    {
        ButtonPressedSound = GetNode("/root/Sounds/ButtonPressed") as AudioStreamPlayer;
        
    }
    public override void _Pressed()
    {
        ButtonPressedSound.Play();
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }
}