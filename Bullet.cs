using Godot;
using System;

public partial class Bullet : Area2D
{

	[Export] float speed = 100.0f;
	Vector2 movement = new Vector2(0, -1);
	public override void _Ready()
	{
	}

  public override void _PhysicsProcess(double delta)
  {
		GlobalPosition += movement.Rotated(Rotation) * speed * (float) delta;
  }
	private void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		Console.WriteLine("Bullet freed");
		QueueFree();
	}
}
