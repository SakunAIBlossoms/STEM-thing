using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;

public partial class PlayButton : Button
{
    private AudioStreamPlayer ButtonPressedSound;

    public override void _Ready()
    {
        ButtonPressedSound = GetNode("/root/Sounds/ButtonPressedSound") as AudioStreamPlayer;
    }
    public override void _Pressed()
    {
        ButtonPressedSound.Play();
        GetTree().ChangeSceneToFile("res://Scenes/Interlude.tscn");
    }	
    
}
