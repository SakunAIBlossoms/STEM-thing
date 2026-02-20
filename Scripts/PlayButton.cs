using Godot;

public partial class PlayButton : Button
{
	private AudioStreamPlayer ButtonPressedSound;

	public override void _Ready()
	{
		ButtonPressedSound = GetNode<AudioStreamPlayer>("ButtonPressedSound");
	}
	public override void _Pressed()
	{
		ButtonPressedSound.Play();
		GetTree().ChangeSceneToFile("res://Scenes/Interlude.tscn");
	}
}
