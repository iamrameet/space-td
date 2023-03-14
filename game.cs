using Godot;
using System;

public partial class game : Node2D
{
	Node2D bullets;
	Player player;
  public override void _Ready()
  {
    bullets = GetNode<Node2D>("Bullets");
    player = GetNode<Player>("Player");
    player.Connect("bulletFire", new Callable(this, "onBulletFire"));
  }
  private void onBulletFire(Bullet bullet){
    bullets.AddChild(bullet);
  }
}
