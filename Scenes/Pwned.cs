using Godot;
using System;

public partial class Pwned : TextureRect
{
	AnimatedSprite2D anim;
	AudioStreamPlayer sfx;
	public override void _Ready()
	{
		anim = GetNode("LAUGH") as AnimatedSprite2D;
		sfx = GetNode("SFX") as AudioStreamPlayer;
		anim.Play("idle");
	}

	public void OnTimerTimeout()
	{
		if (this.Visible)
		{
			sfx.Play();
		}
	}
}
