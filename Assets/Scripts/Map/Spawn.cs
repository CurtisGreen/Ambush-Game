using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : Object {
	/// <summary>
	/// Spawn position
	/// </summary>
	public Vector3 position;

	/// <summary>
	/// Time till enemy spawns at position
	/// </summary>
	public int waitTime;

	/// <summary>
	/// Spawned
	/// </summary>
	public bool hasSpawned = false;
}
