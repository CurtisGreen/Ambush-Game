using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOneManager : ArenaManager {

    // Use this for initialization
    void Start () {
        spawnPositions = new List<Vector3>();
        
        spawnPositions.Add(new Vector3(1006, 10, -705));
        spawnPositions.Add(new Vector3(739, 10, -608));
        spawnPositions.Add(new Vector3(1231, 10, -645));
        spawnPositions.Add(new Vector3(574, 10, -341));
        spawnPositions.Add(new Vector3(1063, 10, -329));

		difficulty = PlayerPrefs.GetInt("Difficulty");

        GenerateWave();
	}
}
