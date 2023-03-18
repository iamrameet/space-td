using Godot;
using System;

public partial class Mob : RigidBody2D
{
	[Export] float minSpeed = 10.0f;
	[Export] float maxSpeed = 100.0f;

	public Vector2 target;
	public override void _Ready()
	{
		target = GetViewportRect().Size / 2;
		Scale = Vector2.One / 2;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = target - Position;
		Rotation = direction.Angle() + MathF.PI / 2;
		Vector2 movement = direction.Normalized() * minSpeed * (float) delta;
		float length = direction.Length();
		if(length > 200){
			LinearVelocity += movement;
			// ApplyForce(movement * 10);
		}else{
			LinearVelocity = LinearVelocity.MoveToward(Vector2.Zero, 3);
		}
		LinearVelocity = LinearVelocity.LimitLength(maxSpeed);
		// Position += movement;
	}
}
