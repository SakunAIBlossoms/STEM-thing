using System;
using Godot;

public partial class ShipStats : SubViewport
{
	[Signal]
	public delegate void ShipStatsLoadedEventHandler();
	// Internal
	string[] objectlist = ["External Bay", "Fuel Tank", "Back Glass Window", "Hardware", "Software", "Communication", "Life Support"];
	int curObj = -1;

	// Public
	public double Integrity = 100.0;

	public bool canBegin = false;

	// Objects
	private RichTextLabel ScanningLabel;
	private ProgressBar bar;
	private RichTextLabel LowIntegrityAlert;
	private Timer timerthatdoesstuffidk;

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
			timerthatdoesstuffidk = GetNode<Timer>("Timer");
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
			LowIntegrityAlert.Visible = true;
		}
	}
	private void ChangeText()
	{
		ScanningLabel.Text = "[pulse freq=0.6 color=#ffffff80]Scanning "+objectlist[curObj];
	}
	private void OnTimerTimeout()
	{
		if (curObj < objectlist.Length)
		{
			curObj++;
			ChangeText();
		}
		else
		{
			ScanningLabel.Visible = false;
			bar.Indeterminate = false;
			bar.Value = Integrity;
			EmitSignal("ShipStatsLoaded");
			timerthatdoesstuffidk.QueueFree();
		}
	}
}
