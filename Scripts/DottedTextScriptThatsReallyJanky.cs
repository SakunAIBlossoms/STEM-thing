using Godot;
using System;
using System.Threading;

public partial class DottedTextScriptThatsReallyJanky : RichTextLabel
{
	private int count = 0;
	string[] textlist = [
		"[shake rate=10.0 level=20 connected=1]Locating Destination[/shake]",
		"[shake rate=10.0 level=20 connected=1]Locating Destination.[/shake]",
		"[shake rate=10.0 level=20 connected=1]Locating Destination..[/shake]",
		"[shake rate=10.0 level=20 connected=1]Locating Destination...[/shake]"
	];
	// Called when the node enters the scene tree for the first time.
	private void ChangeTextTimeout()
	{
		count++;
		if (count > 3) count = 0;
		Text = textlist.GetValue(count) as string;
	}
}
