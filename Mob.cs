using Godot;
using System;

public partial class Mob : RigidBody2D
{
	[Export] float minSpeed = 10.0f;
	[Export] float maxSpeed = 100.0f;

	bool canFire = true;
	public Vector2 target;
	Node2D muzzle;
	PackedScene bulletScene;
	[Signal] public delegate void bulletFireEventHandler(Bullet bullet);
	public override void _Ready()
	{
		target = GetViewportRect().Size / 2;
		Scale = Vector2.One / 2;
		muzzle = GetNode<Node2D>("Muzzle");
		ResourcePreloader loader = new ResourcePreloader();
		bulletScene = GD.Load<PackedScene>("res://bullet.tscn");
	}

	public override void _Process(double delta)
	{
		tryFireBullet();
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
	private async void tryFireBullet(){
		if(canFire){
			canFire = false;
			fireBullet();
			await ToSignal(GetTree().CreateTimer(2), "timeout");
			canFire = true;
		}
	}
	private void fireBullet(){
		Bullet bullet = bulletScene.Instantiate<Bullet>();
		bullet.GlobalPosition = muzzle.GlobalPosition;
		bullet.Rotation = Rotation;
		EmitSignal("bulletFire", bullet);
	}
}
