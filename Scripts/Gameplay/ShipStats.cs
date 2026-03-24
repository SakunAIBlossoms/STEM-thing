using System;
using Godot;

public partial class ShipStats : SubViewport
{
	// Internal
	string[] objectlist = ["External Bay", "Fuel Tank", "Back Glass Window", "Hardware", "Software", "Communication", "Life Support"];
	int curObj = -1;

	// Public
	public double Integrity = 0.0;

	// Objects
	private RichTextLabel ScanningLabel;
	private ProgressBar bar;
	private RichTextLabel LowIntegrityAlert;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetObjects();
		curObj++;
	}

	private bool SetObjects()
	{
		try
		{
			ScanningLabel = GetNode<RichTextLabel>("Scanning");
			bar = GetNode<ProgressBar>("Integrity");
			LowIntegrityAlert = GetNode<RichTextLabel>("DangerWarning");
			return true;
		}
		catch
		{
			return false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Integrity <= 25.0)
		{
			
		}
	}
	private void ChangeText()
	{
		
	}
	private void OnTimerTimeout()
	{
		
	}
}
