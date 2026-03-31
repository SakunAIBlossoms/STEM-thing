using Godot;
using System;

public partial class Overlay : CanvasLayer
{
	TextureRect Autosave;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Autosave = GetNode("Autosave") as TextureRect;
	}

	public void ShowAutoSave()
	{
		var twn = GetTree().CreateTween();
		twn.SetEase(Tween.EaseType.InOut);
		twn.SetTrans(Tween.TransitionType.Expo);
		twn.TweenProperty(Autosave, "position:y", 0.0, 0.8);
		GetTree().CreateTimer(2.5, true, true, true).Timeout += () =>
		{
			var twnout = GetTree().CreateTween();
			twnout.SetEase(Tween.EaseType.InOut);
			twnout.SetTrans(Tween.TransitionType.Expo);
			twnout.TweenProperty(Autosave, "position:y", -342.0, 0.8);
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
