using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] float speed = 50.0f;
	[Export] float rotationSpeed = 2.0f;
	[Export] float maxSpeed = 150.0f;
	[Export] float acceleration = 10.0f;
	bool canFire = true;
	int rotation = 0;
	Vector2 destination = Vector2.Zero;
	public ProgressBar healthBar;
	Node2D muzzle;
	PackedScene bulletScene;
	[Signal] public delegate void bulletFireEventHandler(Bullet bullet);

	public override void _Ready()
	{
		Console.WriteLine("[Player]: ready");
		muzzle = GetNode<Node2D>("Muzzle");
		healthBar = GetNode<ProgressBar>("HealthBar");
		ResourcePreloader loader = new ResourcePreloader();
		bulletScene = GD.Load<PackedScene>("res://bullet.tscn");
	}

	public override void _Process(double delta)
	{
		// QueueRedraw();
	}

	public override void _PhysicsProcess(double delta){
		Vector2 direction = Vector2.Zero;
		handleInput(ref direction);
		destination = direction.Rotated(Rotation) * speed * (float) delta;
		if(direction.Y == 0){
			Velocity = Velocity.MoveToward(destination, 5);
		}else{
			applyAcceleration(destination * acceleration);
		}
		Rotate(rotation * rotationSpeed * (float) delta);
		MoveAndSlide();
	}

	private void applyAcceleration(Vector2 acceleration){
		Velocity += acceleration;
		Velocity = Velocity.LimitLength(maxSpeed);
	}

	private void handleInput(ref Vector2 direction){

		// direction.X = Input.GetActionStrength("moveRight") - Input.GetActionStrength("moveLeft");
		direction.Y = Input.GetActionStrength("moveDown") - Input.GetActionStrength("moveUp");

		rotation = 0;
		if(Input.IsActionPressed("moveLeft"))
			rotation -= 1;
		if(Input.IsActionPressed("moveRight"))
			rotation += 1;

		if(Input.IsActionPressed("fire")){
			tryFireBullet();
		}
	}

	private async void tryFireBullet(){
		if(canFire){
			canFire = false;
			fireBullet();
			await ToSignal(GetTree().CreateTimer(0.2), "timeout");
			canFire = true;
		}
	}
	private void fireBullet(){
		Bullet bullet = bulletScene.Instantiate<Bullet>();
		bullet.GlobalPosition = muzzle.GlobalPosition;
		bullet.Rotation = Rotation;
		EmitSignal("bulletFire", bullet);
	}

  public override void _Draw()
  {
		DrawLine(Vector2.Zero, -Velocity, Colors.White, 1);
  }

}
