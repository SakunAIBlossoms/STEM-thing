using System.Collections.Generic;
using Godot;

public partial class PlayButton : Button
{
    private ColorRect OverlayFade;
    Dictionary<string, AudioStreamPlayer> Sounds;
    public override void _Ready()
    {
        Sounds = new Dictionary<string, AudioStreamPlayer>
        {
            { "Pressed", GetNode("/root/Sounds/ButtonPressed") as AudioStreamPlayer },
            {"Hovered", GetNode("/root/Sounds/ButtonHover") as AudioStreamPlayer},
            {"HoverStopped", GetNode("/root/Sounds/ButtonHoverStopped") as AudioStreamPlayer}
        };
        
        OverlayFade = GetNode("/root/Overlay/Fade") as ColorRect;
    }
    public override void _Pressed()
    {
        Sounds["Pressed"].Play();
        this.Disabled = true;
        Tween FadeOut = GetTree().CreateTween();
        FadeOut.TweenProperty(OverlayFade, "color:a", 1.0, 1.2);
        GetTree().CreateTimer(1.2).Timeout += TweenFinished;
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
    public void TweenFinished()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Interlude.tscn");
    }
}
