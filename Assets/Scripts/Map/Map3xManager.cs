using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3xManager : ArenaManager {

    // Use this for initialization
    void Start () {
        spawnPositions = new List<Vector3>();

        spawnPositions.Add(new Vector3(1792, 10, -1013));
        spawnPositions.Add(new Vector3(2465, 10, -1484));
        spawnPositions.Add(new Vector3(3415, 10, -843));
        spawnPositions.Add(new Vector3(2884, 10, -1884));
        spawnPositions.Add(new Vector3(2124, 10, -1575));

        difficulty = PlayerPrefs.GetInt("Difficulty");

        GenerateWave();

		points = 0;
		multiplier = 1;
	}

}
