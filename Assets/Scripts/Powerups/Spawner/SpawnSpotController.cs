using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpotController : MonoBehaviour
{
	public static SpawnSpot randomSpawn { get { return spawnSpots[Random.Range(0,spawnSpots.Count)]; }}
	public static List<SpawnSpot> spawnSpots;

	public static void RemoveSpawner (SpawnSpot spawner)
	{
		spawnSpots.Remove(spawner);
	}

	public void Awake ()
	{
    	spawnSpots = new List<SpawnSpot>(Object.FindObjectsOfType<SpawnSpot>());
	}

}
