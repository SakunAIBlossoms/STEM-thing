using Godot;
using System;

public partial class DottedTextScriptThatsReallyJanky : RichTextLabel
{
	private int count = 0;
	private Timer DotDelay;

	public override void _Ready()
	{
		DotDelay = GetNode<Timer>("DotDelay");
		DotDelay.Start();
	}
	string[] textlist = [
		"[shake rate=10.0 level=20 connected=1]Locating Destination[/shake]",
		"[shake rate=10.0 level=20 connected=1]Locating Destination.[/shake]",
		"[shake rate=10.0 level=20 connected=1]Locating Destination..[/shake]",
		"[shake rate=10.0 level=20 connected=1]Locating Destination...[/shake]"
	];
	private void ChangeTextTimeout()
	{
		count++;
		if (count > 3) count = 0;
		Text = textlist.GetValue(count) as string;
	}
}
