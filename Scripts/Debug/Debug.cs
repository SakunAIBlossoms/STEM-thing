using Godot;
using ImGuiNET;
using System;

public partial class Debug : CanvasLayer
{
	double ElapsedTime = -1.0;
	RichTextLabel label;
	public override void _Ready()
	{
		// Lets make sure the label variable isnt null so we can actually display it LMAO
		label = GetNode("Info") as RichTextLabel;
		if (!OS.IsDebugBuild()) {
			GD.PushWarning("Game is not in debug mode. Removing debugging UI elements...");
			this.QueueFree();
		}
	}

	// Check every frame for the framerate and current memory usage, then apply it to the FPS counter
	public override void _Process(double delta)
	{
		ElapsedTime += delta;
		if (label != null && ElapsedTime % 4 == 0)
		{
			label.Text = "FPS:" + Engine.GetFramesPerSecond().ToString() + " | Memory: " + (OS.GetStaticMemoryUsage() / 1000000).ToString() + "mb";
		}
	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event.AsText() == "F3" && @event.IsReleased() && OS.IsDebugBuild())
		{
			label.Visible = !label.Visible;
		}
    }

}
