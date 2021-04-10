using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpot : MonoBehaviour
{
	public Transform spawnTransform;
	public DestructableBuilding building;
	public bool functional { get { return !building.isDestroyed; }}

	public IPowerup hostedPowerup = null;

	private bool removed = false;

	public void Update ()
	{
		if (hostedPowerup != null && building.isAblaze)
		{
			hostedPowerup.Destroy();
			hostedPowerup = null;
		}

		if (!removed && !functional)
		{
			removed = true;
			SpawnSpotController.RemoveSpawner(this);
		}

	}
}
