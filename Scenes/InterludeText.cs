using Godot;
using System;

public partial class InterludeText : RichTextLabel
{
	private int count = 0;
	string[] textlist = [
		"CLICK TO CONTINUE",
		"",
	];
	// Called when the node enters the scene tree for the first time.
	private void FlashingTimeout()
	{
		count++;
		if (count > 1) count = 0;
		Text = textlist.GetValue(count) as string;
	}
}
