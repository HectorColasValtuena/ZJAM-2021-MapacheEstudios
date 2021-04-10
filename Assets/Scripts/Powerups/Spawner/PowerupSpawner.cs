using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
	private const float minimumInterval = 5f;
	private const float maximumInterval = 35f;

	[SerializeField]
	private PowerupBase[] basicPowerups;

	private float timer;

	public void Awake ()
	{
		ResetTimer();
	}

	public void Update ()
	{
		UpdateBasicTimer ();
	}

	private void UpdateBasicTimer ()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			SpawnBasicPowerup();
			ResetTimer();
		}
	}

	private void SpawnBasicPowerup ()
	{
		Debug.LogWarning("spawning powerup");
		SpawnPowerup (basicPowerups[Random.Range(0, basicPowerups.Length)]);
	}

	private void ResetTimer ()
	{
		timer = Random.Range(minimumInterval,maximumInterval);
	}

	private void SpawnPowerup (IPowerup powerup)
	{
		SpawnSpot spawnSpot = SpawnSpotController.randomSpawn;
		
		IPowerup newPowerup = Instantiate(powerup.gameObject, spawnSpot.spawnTransform.position, Quaternion.identity).GetComponent<IPowerup>();

		spawnSpot.hostedPowerup = newPowerup;
	}
}
