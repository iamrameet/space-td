using Godot;
using System;

public partial class Bullet : Area2D
{

	[Export] float speed = 1000.0f;
	[Export] float maxSpeed = 1000.0f;
	Vector2 velocity = Vector2.Zero;
	Vector2 movement = new Vector2(0, -1);
	public override void _Ready()
	{
	}

  public override void _PhysicsProcess(double delta)
  {
		velocity = movement * speed * (float) delta;
		velocity = velocity.LimitLength(maxSpeed);
		GlobalPosition += velocity.Rotated(Rotation);
  }
	private void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		Console.WriteLine("Bullet freed");
		QueueFree();
	}
	private void _on_body_entered(Node2D body)
	{
		Mob mob = (Mob) body;
		body.QueueFree();
		QueueFree();
	}
}
