using Godot;
using System;

public partial class Debug : CanvasLayer
{
	RichTextLabel label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		label = GetNode("Info") as RichTextLabel;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (label != null)
		{
			label.Text = "FPS:" + Engine.GetFramesPerSecond().ToString() + " | Memory: " + (OS.GetStaticMemoryUsage() / 1000000).ToString() + "mb";
			//Math.Round(Convert.ToDecimal(OS.GetStaticMemoryUsage() / 1000000), 2);
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
