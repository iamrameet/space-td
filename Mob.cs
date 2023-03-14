using Godot;
using System;

public partial class Mob : RigidBody2D
{
	[Export] float minSpeed = 150.0f;
	[Export] float maxSpeed = 150.0f;

	public override void _Ready()
	{
	}

}
