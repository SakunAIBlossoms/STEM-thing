using System;
using Godot;

public partial class MouseCursor : CanvasLayer
{
	// Set variables
	Sprite2D Cursor;
	GpuParticles2D Particles;
	Vector2 FinalPos = new Vector2(0,0);
	// Make it so the lerp speed can be changed at will
	int LerpSpeed = 12;
	// Check whether its pressed or not lol
	bool Pressed = false;

	public override void _Ready()
	{
		// Assign the variables when the scene is ready, dont want any errors now do we?
		Cursor = GetNode("Cursor") as Sprite2D;
		Particles = Cursor.GetNode("Particles") as GpuParticles2D;

	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseButton mouseButton)
		{
			Pressed = mouseButton.IsPressed();
		}
		if (@event is InputEventMouseMotion eventmotion)
		{
			FinalPos = eventmotion.Position;
		}
    }

	public override void _Process(double delta)
	{
		if (Pressed) Cursor.ApplyScale(new Vector2((float) Mathf.Lerp(Cursor.Scale.X, 0.8, LerpSpeed * delta), (float) Mathf.Lerp(Cursor.Scale.X, 0.8, LerpSpeed * delta)));
		else if (!Pressed) Cursor.ApplyScale(new Vector2((float) Mathf.Lerp(Cursor.Scale.X, 1, LerpSpeed * delta), (float) Mathf.Lerp(Cursor.Scale.X, 1, LerpSpeed * delta)));

			if (Cursor.Position != FinalPos)
			{
				if (!Particles.Emitting) Particles.Emitting = true;
				Cursor.Position = new Vector2((float)Mathf.Lerp(Cursor.Position.X, FinalPos.X, LerpSpeed * delta), (float)Mathf.Lerp(Cursor.Position.Y, FinalPos.Y, LerpSpeed * delta));
			}
			else if (Cursor.Position == FinalPos)
			{
				if (Particles.Emitting) Particles.Emitting = false;
			}
	}
}
