using System;
using Godot;

public partial class MouseCursor : CanvasLayer
{
	// Set variables
	// Cursor sprite itself
	Sprite2D Cursor;
	// The particles that show up when moving your mouse
	GpuParticles2D Particles;
	Vector2 FinalPos = new Vector2(0,0);
	// Make it so the lerp speed can be changed at will
	int LerpSpeed = 12;
	// Check whether its pressed or not and the scale to set it to if it is being pressed lol
	double PressedScale = 0.3;
	bool Pressed = false;
	bool PrintDebuggingInformation = false;

	public override void _Ready()
	{
		// Assign the variables when the scene is ready, dont want any errors now do we?
		Cursor = GetNode("Cursor") as Sprite2D;
		Particles = Cursor.GetNode("Particles") as GpuParticles2D;
		if (Cursor != null) Input.MouseMode = Input.MouseModeEnum.Hidden;
		PrintDebuggingInformation = OS.IsDebugBuild();
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventKey key) {
			Cursor.Visible = false;
		}
		if (@event is InputEventMouseButton mouseButton)
		{
			Pressed = mouseButton.IsPressed();
		}
		if (@event is InputEventMouseMotion eventmotion)
		{
			FinalPos = eventmotion.Position;
			Cursor.Visible = true;
		}
    }

	public override void _Process(double delta)
	{
		if (Pressed) Cursor.Scale = new Vector2((float) Mathf.Lerp(Cursor.Scale.X, PressedScale, LerpSpeed * delta), (float) Mathf.Lerp(Cursor.Scale.Y, PressedScale, LerpSpeed * delta));
		else if (!Pressed) Cursor.Scale = new Vector2((float) Mathf.Lerp(Cursor.Scale.X, 0.5, LerpSpeed * delta), (float) Mathf.Lerp(Cursor.Scale.Y, 0.5, LerpSpeed * delta));
		
		if (Cursor.Position != FinalPos)
		{
			if (!Particles.Emitting)
			{
				Particles.Emitting = true;
				if (PrintDebuggingInformation) GD.Print("Enable Particles");
			}
			if (PrintDebuggingInformation)
			{
				GD.Print("Cursor X: ", Math.Round(Cursor.Position.X), " ~ ", FinalPos.X);
				GD.Print("Cursor Y: ", Math.Round(Cursor.Position.Y), " ~ ", FinalPos.Y);
			}
			Cursor.Position = new Vector2((float)Mathf.Lerp(Cursor.Position.X, FinalPos.X, LerpSpeed * delta), (float)Mathf.Lerp(Cursor.Position.Y, FinalPos.Y, LerpSpeed * delta));
		}
		// Here is a print to constantly check if the positions are matching or not, this will cause immense lag therefore it will be ignored unless a specific flag is set
		if (PrintDebuggingInformation) GD.Print("Matching Positions? = ", Math.Round(Cursor.Position.X) == FinalPos.X && Math.Round(Cursor.Position.Y) == FinalPos.Y);
		if (Math.Round(Cursor.Position.X) == FinalPos.X && Math.Round(Cursor.Position.Y) == FinalPos.Y)
		{
			Particles.Emitting = false;
			if (PrintDebuggingInformation) GD.Print("Disable Particles");
		}
	} 
}
