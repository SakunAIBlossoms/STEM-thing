using Godot;
using ImGuiNET;

public partial class Debug : CanvasLayer
{
	double ElapsedTime = -1.0;
	float d;
	RichTextLabel label;
	public override void _Ready()
	{
		if (!OS.IsDebugBuild()) this.QueueFree();
	}

	// Check every frame for the framerate and current memory usage, then apply it to the FPS counter
	public override void _Process(double delta)
	{
		ElapsedTime += delta;
		d = (float) delta;
		ImGui.SetNextWindowSizeConstraints(new System.Numerics.Vector2(320, 200), new System.Numerics.Vector2(320, 200));
		if (ImGui.Begin("Debug UI", ImGuiWindowFlags.NoResize))
		{
			ImGui.SetWindowFontScale(1.2f);
			ImGui.Text("FPS: " + Engine.GetFramesPerSecond().ToString());
			ImGui.Text("Memory Usage: "+(OS.GetStaticMemoryUsage() / 1000000).ToString() + "mb");
			ImGui.PlotLines("Frame Times", ref d, 60);
        	ImGui.End();
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
