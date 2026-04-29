using Godot;
using System.Collections.Generic;
public partial class OptionsButton : Button
{
    Dictionary<string, AudioStreamPlayer> Sounds;
    public override void _Ready()
    {
        Sounds = new Dictionary<string, AudioStreamPlayer>
        {
            { "Pressed", GetNode("/root/Sounds/ButtonPressed") as AudioStreamPlayer },
            {"Hovered", GetNode("/root/Sounds/ButtonHover") as AudioStreamPlayer},
            {"HoverStopped", GetNode("/root/Sounds/ButtonHoverStopped") as AudioStreamPlayer}
        };
    }
    public override void _Pressed()
    {
        Sounds["Pressed"].Play();
        GetTree().ChangeSceneToFile("res://Scenes/OptionsMenu.tscn");
    }	
    public void OnMouseEntered()
    {
        Sounds["Hovered"].Stop();
        Sounds["Hovered"].PitchScale = 1f;
        Sounds["Hovered"].Play();
    }
    public void OnMouseExited()
    {
        Sounds["HoverStopped"].Stop();
        Sounds["HoverStopped"].PitchScale = 1f;
        Sounds["HoverStopped"].Play();
    }
}