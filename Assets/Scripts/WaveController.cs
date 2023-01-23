using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WaveController : MonoBehaviour
{
    public static WaveController instance;
	private void Awake()
	{
        instance = this;
	}

	public List<Wave> waves;
    [SerializeField]
    private float waveInterval = 30f;
    [SerializeField]
    private Transform spawnLocation;
    [SerializeField]
    private Transform targetLocation;
    private List<GameObject> enemies;

    [SerializeField] [Tooltip("How many additional enemies to spawn for each wave after we've wrapped.")]
    private int loopSpawnCount = 2;

    private bool spawning = false;
    private bool waiting = false;

    private int numsIteratedWaves = 0;

    [HideInInspector]
    public int waveIndex = 0;
    [HideInInspector]
    public float timeUntilWave;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();

        StartCoroutine(nameof(WaveTimer), waveInterval);
    }

    void Update()
	{
        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject go in enemies)
		{
            if (!go)
                toRemove.Add(go);
		}

        foreach (GameObject go in toRemove)
            enemies.Remove(go);


        if (!spawning && enemies.Count == 0 && !waiting)
		{
            print("WOO");
            StartCoroutine(nameof(WaveTimer), waveInterval);
        }
	}


    IEnumerator SpawnWave(Wave wave)
	{
        spawning = true;
        float timeWaited = 0f;
        int numberSpawned = 0;
        while (numberSpawned < wave.count + numsIteratedWaves*loopSpawnCount)
		{
            while (timeWaited < wave.spawnInterval)
			{
                yield return new WaitForSeconds(0.05f);
                timeWaited += 0.05f;
			}

            GameObject go = Instantiate(wave.enemyPrefab);
            go.transform.position = spawnLocation.position;
            go.GetComponent<AIDestinationSetter>().target = targetLocation;
            enemies.Add(go);

            timeWaited = 0f;
            numberSpawned++;
		}

        spawning = false;
	}

    IEnumerator WaveTimer(float waitTime)
	{
        waiting = true;
        timeUntilWave = waitTime;
        while (timeUntilWave > 0)
        {
            yield return new WaitForSeconds(0.1f);
            timeUntilWave -= 0.1f;
        }

        if (waveIndex >= waves.Count)
        {
            numsIteratedWaves++;
            waveIndex = 0;
        }

        StartCoroutine(nameof(SpawnWave), waves[waveIndex++]);

        waiting = false;
    }


    [Serializable]
    public struct Wave
    {
        [Tooltip("Enemy prefab to spawn.")]
        public GameObject enemyPrefab;
        [Tooltip("Number of prefab to spawn.")]
        public int count;
        [Tooltip("Time between each enemy spawn. Minimum time = 0.05")]
        public float spawnInterval;
	}
}
