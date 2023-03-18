using Godot;
using System;

public partial class MobSpawner : StaticBody2D
{
  [Export] public PackedScene MobScene;
  [Export] public float rotationSpeed = 1;
	[Signal] public delegate void mobSpawnEventHandler(Mob mob);

  public override void _Process(double delta)
  {
	Rotation += rotationSpeed * (float) delta;
  }
  public void spawnMob()
  {
		Mob spawnedMob = MobScene.Instantiate<Mob>();
		EmitSignal(nameof(mobSpawn), spawnedMob, this);
  }
}
