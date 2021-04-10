using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArsonController : MonoBehaviour
{
//Constants
	private const int maxSearchAttempts = 6;

	private const float arsonVirulence = 0.3f;

	private const float radiusIncrease = 0.25f;	//radius increase per interval
	private const float frequencyIncrease = 1f;	//arson incidence per interval
	private const float additionalFireIncrease = 1.5f;
	private const float baseInterval = 60f;		//interval length in seconds
//ENDOF Constants

//private properties
	private float arsonInterval { get { return baseInterval / currentFrequency; }}
//ENDOF private properties

//private variables
	[SerializeField]
	private LayerMask burnableLayerMask;

	[SerializeField]
	private GameObject explosionPrefab;

	private float currentRadius = 1.0f;
	private float currentFrequency = 5f;
	private float additionalFires = 0.35f;

	private float intervalTimer = 0f;
	private float arsonTimer = 0f;
//ENDOF private variables

//MonoBehaviour lifecycle
	public void Update ()
	{
		UpdateInterval();
		UpdateArson();
	}
//ENDOF MonoBehaviour lifecycle

//private methods
	private void UpdateInterval ()
	{
		intervalTimer += Time.deltaTime;
		if (intervalTimer >= baseInterval)
		{
			intervalTimer -= baseInterval;
			NewInterval();
		}
	}

	private void NewInterval ()
	{
		currentRadius += radiusIncrease;
		currentFrequency += frequencyIncrease;
		additionalFires += additionalFireIncrease;
	}

	private void UpdateArson ()
	{
		arsonTimer += Time.deltaTime;
		if (arsonTimer >= arsonInterval)
		{
			Arson(FindArsonTarget());
			arsonTimer = 0;
		}
	}

	//try several times for a standing building
	//if none found return last candidate
	private IBurnable FindArsonTarget ()
	{
		IBurnable candidate = null;
		for (int i = 0; i < maxSearchAttempts; i++)
		{
			candidate = BurnableCache.randomBurnable;
			IDestructable destructable = candidate as IDestructable;
			if (destructable != null && !destructable.isDestroyed)
			{
				return candidate;
			}
		}

		return candidate;
	}

	private void Arson (IBurnable target)
	{
		Ignite(target);

		float gasLeft = additionalFires;
		IBurnable[] burnables = FindBurnablesAroundPoint(target.transform.position);

		while (gasLeft >= 0)
		{
			if (gasLeft >= 1 || Random.Range(0.0f, 1.0f) <= gasLeft)
			{
				Ignite(burnables[Random.Range(0, burnables.Length)]);
			}
		}
	}

	private void Ignite (IBurnable target)
	{target.ChangeVirulence(arsonVirulence);}

	private	IBurnable[] FindBurnablesAroundPoint (Vector3 center)
	{
		RaycastHit[] hits = Physics.SphereCastAll(
			origin: center,
			radius: currentRadius,
			direction: Vector3.up,
			maxDistance: currentRadius,
			layerMask: burnableLayerMask,
			queryTriggerInteraction: QueryTriggerInteraction.Collide

		);

		List<IBurnable> burnables = new List<IBurnable>();

		foreach (RaycastHit hit in hits)
		{
			IBurnable foundBurnable = hit.transform.GetComponentInChildren<IBurnable>();

			if (foundBurnable != null)
			{
				burnables.Add(foundBurnable);
			}
			else { Debug.LogError("CacheBurnableNeightbors: Found burnable with no IBurnable: " + hit.transform.name); }
		}

		return burnables.ToArray();
	}
//ENDOF private methods
}
