using System;
using Godot;

[Icon("res://Images/CheckBox.svg")]
public partial class DdCheckbox : Control
{
	// Add Toggle signal
	[Signal]
	public delegate void ToggledEventHandler(bool NewState);

	// Add all default variables
	[Export]
	bool Activated = false;
	[Export]
	bool Disabled = false;
	[Export]
	ImageTexture BackgroundTexture = null;
	[Export]
	ImageTexture TickTexture = null;

	private float ModulateFade = 0.6f;
	private bool Hovering = false;

	// Add all objects
	private TextureRect BGTexture;
	private TextureRect Tick;
	private AudioStreamPlayer Sound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Continue with setting all the variables.
		BGTexture = GetNode<TextureRect>("Background");
		Tick = GetNode<TextureRect>("Overlay");
		Sound = GetNode<AudioStreamPlayer>("Sound");

		// Change everything that needs to be changed.
		if (Disabled)
		{
			Modulate = new Color(1, 1, 1, ModulateFade);
		}
		Tick.Visible = Activated;
	}
	public void Disable()
	{
		Disabled = true;
		Modulate = new Color(1, 1, 1, ModulateFade);
	}
	public void Toggle()
	{
		if (!Disabled)
		{
			Activated = !Activated;
			if (Activated)
			{
				Sound.PitchScale = 1.2f;
				Sound.Play();
				Tick.Scale = new Vector2(0.8f, 0.8f);
			}
			else
			{
				Sound.PitchScale = 0.8f;
				Sound.Play();
			}
			Tick.Visible = Activated;
		}
	}
	private void GuiInputCallback(InputEvent @event)
	{
		if (@event is InputEventMouseButton)
		{
			if (@event.IsPressed())
			{
				Toggle();
			}
		}
	}
	private void OnMouseEntered()
	{
		Hovering = true;
	}
	private void OnMouseExited()
	{
		Hovering = false;
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Tick.Scale.X != 1 && Tick.Scale.Y != 1) Tick.Scale = new Vector2((float)Mathf.Lerp(Tick.Scale.X, 1, 8 * delta), (float)Mathf.Lerp(Tick.Scale.Y, 1, 8 * delta)); // constantly scale the tick up to the right size
		if (!Disabled)
		{
			if (Hovering)
			{
				Modulate = new Color((float)Mathf.Lerp(Modulate.R, 0.8, 12 * delta), (float)Mathf.Lerp(Modulate.G, 0.8, 12 * delta), (float)Mathf.Lerp(Modulate.B, 0.8, 12 * delta));
			}
			if (!Hovering)
			{
				Modulate = new Color((float)Mathf.Lerp(Modulate.R, 1, 12 * delta), (float)Mathf.Lerp(Modulate.G, 1, 12 * delta), (float)Mathf.Lerp(Modulate.B, 1, 12 * delta));
			}
		}
	}
}
