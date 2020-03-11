using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour {
	/// <summary>
	/// Skeleton prefab
	/// </summary>
	public GameObject skeleton;

	/// <summary>
	/// Spider prefab
	/// </summary>
	public GameObject spider;

	/// <summary>
	/// Player prefab
	/// </summary>
	public GameObject player;

	/// <summary>
	/// Player's points
	/// </summary>
	public static int points = 0;

	/// <summary>
	/// Player's multiplier of the score made. It disappers whenever player get's damaged.
	/// Multiply by 2 if want to increase. Min number it can be is 1
	/// </summary>
	public static int multiplier = 1;

	/// <summary>
	/// The wave count player is on
	/// </summary>
	protected int wave = 1;

    /// <summary>
	/// Number of enemies.
	/// </summary>
	protected int enemiesCount = 5;

    /// <summary>
	/// Enemies
	/// </summary>
	protected List<GameObject> enemies = new List<GameObject>();

    /// <summary>
    /// Spawn positions. Should be only 3. Initialize at the start.
    /// </summary>
    protected List<Vector3> spawnPositions;

	/// <summary>
	/// The difficulty.
	/// </summary>
	protected int difficulty;

    /// <summary>
    /// Min spawn time
    /// </summary>
    protected int minWaitTime = 3;

    /// <summary>
    /// Max spawn time
    /// </summary>
    protected int maxWaitTime = 6;

    /// <summary>
    /// Max spawn time
    /// </summary>
    protected float time = 0;

	/// <summary>
	/// Bool to determine if wave is currently running.
	/// </summary>
	protected bool isWaveInProgress = false;

    // Use this for initialization
    void Start ()
    {
        // WARNING: This is going to be overwritten by classes that inherit this class.
    }
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("UPDATING: " + points);
		Text waveText = player.transform.Find ("Canvas").Find("WaveText").GetComponent<Text>();

		waveText.text = "Points: " + points + " " + multiplier + "x";

		bool isEnd = EndOfRound();

		if (!isEnd) {
			time += Time.deltaTime;
			Spawn ();
		} else {
			if (!isWaveInProgress) {
				GenerateNewWave ();
			}
		}
    }

    /// <summary>
	/// Start waves generation
	/// </summary>
	public void GenerateWave() {
		isWaveInProgress = true;

        for (int i = 0; i < enemiesCount; i++) {
            int index = Random.Range(0, 2);
            GameObject enemy;

            Debug.Log("index: " + index);

            // 1. Choose which type of enemy
            if (index == 0)
            {
                enemy = Object.Instantiate(skeleton);
            } else {
                enemy = Object.Instantiate(spider);
            }

            // 2. Choose spawn position
            index = Random.Range(0, spawnPositions.Count);

            enemy.transform.position = spawnPositions[index];

            // 3. Choose spawn time
            index = Random.Range(minWaitTime, maxWaitTime);
            Enemy enemyScript = (Enemy)enemy.GetComponent(typeof(Enemy));
            
            enemyScript.spawnTime = index;

            minWaitTime += 5;
            maxWaitTime += 5;

            // 4. Add into vector
            enemy.SetActive(false);

            enemies.Add(enemy);
        }
    }

    /// <summary>
    /// Spawns enemies in the enemies list.
    /// </summary>
	protected void Spawn() {
        foreach (GameObject enemy in enemies)
        {
			if (enemy != null) {
				
				Enemy enemyScript = (Enemy)enemy.GetComponent (typeof(Enemy));

				if (!enemyScript.hasSpawned && enemyScript.spawnTime < time) {
					enemy.SetActive (true);
					enemyScript.hasSpawned = true;
				}
			}
        }
	}

    /// <summary>
    /// Check if it's the end of round
    /// </summary>
    /// <returns></returns>
	protected bool EndOfRound() {
		bool isEnd = true;

		foreach (GameObject enemy in enemies) {
			if (enemy != null) {
				Enemy enemyScript = (Enemy)enemy.GetComponent(typeof(Enemy));

				if (enemyScript != null) {
					if (enemy.activeInHierarchy || !enemyScript.hasSpawned) {
						isEnd = false;
					}
				}

			}
		}
			
		if (isEnd) {
			isWaveInProgress = false;
		}

		return isEnd;
	}
    
    /// <summary>
    /// Generates the next waves. NOT initial wave.
    /// </summary>
	protected void GenerateNewWave() {
		enemies.Clear();
		wave++;
		enemiesCount *= 2;
		minWaitTime = 5;
		maxWaitTime = 10;
		time = 0;

		GenerateWave ();
	}
}
