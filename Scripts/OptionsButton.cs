using Godot;
public partial class OptionsButton : Button
{
	public override void _Pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/OptionsMenu.tscn");
	}
}
