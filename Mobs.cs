using Godot;

public partial class Mobs : Node2D
{
  // Reference to the object to spawn
  [Export] public PackedScene ObjectToSpawn;
	[Signal] public delegate void mobSpawnEventHandler(Mob mob);

  // Spawn objects around the border of the window
  public void SpawnObjectsAroundBorder()
  {
    // Get the size of the window
    Vector2 windowSize = GetViewportRect().Size;

		float random = GD.Randf();

    // Define the spawn positions
    Vector2[] spawnPositions = new Vector2[]
    {
			new Vector2(0, windowSize.Y / 2 * random), // Left
			new Vector2(windowSize.X, windowSize.Y / 2 * random), // Right
			new Vector2(windowSize.X / 2 * random, 0), // Top
			new Vector2(windowSize.X / 2 * random, windowSize.Y) // Bottom
    };

    Vector2 position = spawnPositions[Mathf.FloorToInt(GD.Randf() * spawnPositions.Length)];
		Mob spawnedObject = ObjectToSpawn.Instantiate<Mob>();
		spawnedObject.Position = position;
		EmitSignal(nameof(mobSpawn), spawnedObject);
  }
}
