using Godot;
using System;

public partial class PingSfx : AudioStreamPlayer3D
{
	private void Ping() { this.Play(0.0f); }

}
