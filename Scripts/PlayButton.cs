using System.Threading.Tasks;
using Godot;

public partial class PlayButton : Button
{
    private AudioStreamPlayer ButtonPressedSound;
    private ColorRect OverlayFade;
    public override void _Ready()
    {
        ButtonPressedSound = GetNode<AudioStreamPlayer>("ButtonPressedSound");
        OverlayFade = GetNode("/root/Overlay/Fade") as ColorRect;
    }
    public override void _Pressed()
    {
        ButtonPressedSound.Play();
        Tween FadeOut = GetTree().CreateTween();
        FadeOut.TweenProperty(OverlayFade, "color:a", 1.0, 1.2);
        GetTree().CreateTimer(1.2).Timeout += TweenFinished;
    }
    public void TweenFinished()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Interlude.tscn");
    }
}
