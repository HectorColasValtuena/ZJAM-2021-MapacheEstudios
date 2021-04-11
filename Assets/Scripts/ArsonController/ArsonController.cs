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
	private const float baseInterval = 30f;		//interval length in seconds
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
	public void Awake ()
	{
		StartCoroutine(InitialArson());
	}

	private IEnumerator InitialArson ()
	{
		yield return new WaitForSeconds(1f);
		Arson(FindArsonTarget());
	}

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
		Debug.LogWarning("Arson: " + target.transform.name);
		Ignite(target);
		Instantiate(original: explosionPrefab, position: target.transform.position, rotation: Quaternion.identity);

		float gasLeft = additionalFires;
		IBurnable[] burnables = BurnableCache.FindBurnablesAroundPoint(target.transform.position, currentRadius);

		while (gasLeft >= 0)
		{
			if (gasLeft >= 1 || Random.Range(0.0f, 1.0f) <= gasLeft)
			{
				Ignite(burnables[Random.Range(0, burnables.Length)]);
			}
			gasLeft--;
		}
	}

	private void Ignite (IBurnable target)
	{ target.ChangeVirulence(arsonVirulence); }

	
//ENDOF private methods
}
