using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableCache : MonoBehaviour
{
//    public static BurnableCache instance;
	public static LayerMask burnableLayerMask;

    public static IBurnable[] sceneBurnables;
    public static IBurnable randomBurnable { get { return sceneBurnables[Random.Range(0, sceneBurnables.Length)]; }}

    public static IBurnable[] FindBurnablesAroundPoint (
    	Vector3 center,
    	float radius
    ){
		RaycastHit[] hits = Physics.SphereCastAll(
			origin: center,
			radius: radius,
			direction: Vector3.up,
			maxDistance: radius,
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

	[SerializeField]
	private LayerMask initBurnableLayerMask;

    public void Awake ()
    {
		burnableLayerMask = initBurnableLayerMask;
		sceneBurnables = Object.FindObjectsOfType<BurnableBuilding>();
		Debug.Log("Found " + sceneBurnables.Length + " burnable neighbors");

    }
}
