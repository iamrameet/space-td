using Godot;
using System;

public partial class game : Node2D
{
	Node2D bullets;
	Player player;
  Mobs mobs;
	Node2D mobSpawners;
	Area2D playerBase;
  Timer mobSpawnTimer;
  public override void _Ready()
  {
		bullets = GetNode<Node2D>("Bullets");
		player = GetNode<Player>("Player");
		mobs = GetNode<Mobs>("Mobs");
		mobSpawners = GetNode<Node2D>("MobSpawners");
		playerBase = GetNode<Area2D>("PlayerBase");
		mobSpawnTimer = GetNode<Timer>("MobSpawnTimer");

		player.Connect("bulletFire", new Callable(this, "onBulletFire"));
		foreach(MobSpawner spawner in mobSpawners.GetChildren()){
			spawner.Connect("mobSpawn", new Callable(this, "onMobSpawn"));
			mobSpawnTimer.Connect("timeout", new Callable(spawner, "spawnMob"));
		}
		mobSpawnTimer.Start();
  }
  private void onBulletFire(Bullet bullet){
		bullets.AddChild(bullet);
  }
  private void onMobSpawn(Mob mob, MobSpawner spawner){
		mobs.AddChild(mob);
		const float t = 0.1f;
		Vector2 spawnPosition = (playerBase.Position - spawner.Position) * t + spawner.Position;
		mob.Position = spawnPosition;
  }
  public override void _Process(double delta)
  {
  	float rotationSpeed = 0.5f;
		playerBase.Rotation += rotationSpeed * (float) delta;
  }
  public override void _PhysicsProcess(double delta)
  {
		foreach(Mob mob in mobs.GetChildren()){
			mob.target = playerBase.Position;
		}
  }
}
