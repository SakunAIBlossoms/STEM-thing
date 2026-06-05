using Godot;
using System;

public partial class InterludeText : RichTextLabel
{
	private int count = 0;
	private Timer FlashingText;

	public override void _Ready()
	{
		FlashingText = GetNode<Timer>("FlashingText");
		FlashingText.Start();
	}
	public static string[] Textlist = [
		"CLICK TO CONTINUE",
		"",
	];
	// Called when the node enters the scene tree for the first time.
	private void FlashingTimeout()
	{
		count++;
		if (count > 1) count = 0;
		Text = Textlist.GetValue(count) as string;
	}
}
