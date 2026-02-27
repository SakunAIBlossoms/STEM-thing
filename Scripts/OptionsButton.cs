using Godot;
public partial class OptionsButton : Button
{
    private AudioStreamPlayer ButtonPressedSound;

    public override void _Ready()
    {
        ButtonPressedSound = GetNode("/root/Sounds/ButtonPressedSound") as AudioStreamPlayer;
    }
    public override void _Pressed()
    {
        ButtonPressedSound.Play();
        GetTree().ChangeSceneToFile("res://Scenes/OptionsMenu.tscn");
    }	
    
}