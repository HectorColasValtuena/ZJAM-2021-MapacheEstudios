using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableBuilding :
	BuildingBase,
	IBurnable
{
//Constants
	//every how many seconds should a burning building attempt propagation
	private const float propagationInterval = 1.0f;
//ENDOF Constants

//IBurnable implementation
	//True if building is in flames
	public bool isAblaze { get { return fireVirulence > 0; }}

	//0 to 1 representation of how intense flames are
	public float blazeIntensity
	{
		get
		{
			return (isAblaze)
				? fireVirulence / maximumVirulence
				: 0.0f;
		}
	} 

	//changes intensity of the flames
	public void ChangeVirulence (float fireVirulenceChange, bool applyDeltaTime = false)
	{
		//updates virulence appling time delta if not ignored
		fireVirulence = Mathf.Clamp(
			value: fireVirulence + (fireVirulenceChange * ((applyDeltaTime) ? Time.deltaTime : 1f)),
			min: propagationResistance,
			max: maximumVirulence
		);
	}
//ENDOF IBurnable implementation

//protected hierarchy variables
  //Serialized values
	//[SerializeField]
	protected float propagationResistance = -0.1f;
	//[SerializeField]
	protected float propagationChance = 0.5f;
	//[SerializeField]
	protected float propagationRate = 0.25f;
	//[SerializeField]
	protected float propagationDistance = 3.0f;
	//[SerializeField]
	protected float virulenceGrowth = 0.1f;
	//[SerializeField]
	protected float maximumVirulence = 1f;
  //ENDOF Serialized values

	protected Animator animator;

	[SerializeField]
	protected float fireVirulence = 0.0f;
//ENDOF protected hierarchy variables

//private variables
	[SerializeField]
	private LayerMask burnableLayerMask;

	private float propagationTimer = 0.0f;

	private IBurnable[] burnableNeighbors;
//ENDOF private variables


//MonoBehaviour lifecycle
	public void Awake ()
	{
		animator = GetComponent<Animator>();
		fireVirulence = propagationResistance;

		CacheBurnableNeightbors();
	}

	public void Update ()
	{
		ProcessFire();
	}
//ENDOF MonoBehaviour lifecycle

//public methods
//ENDOF public methods

//Protected hierarchy methods
	protected void ProcessFire ()
	{
		UpdateVirulence();
		TryPropagation();
	}
//ENDOF Protected hierarchy methods

//private methods
	private	void CacheBurnableNeightbors ()
	{
		RaycastHit[] hits = Physics.SphereCastAll(
			origin: transform.position,
			radius: propagationDistance,
			direction: Vector3.up,
			maxDistance: propagationDistance,
			layerMask: burnableLayerMask
			//queryTriggerInteraction:

		);

		List<IBurnable> burnables = new List<IBurnable>();

		foreach (RaycastHit hit in hits)
		{
			if (hit.transform == this.transform) { continue; } //ignore oneself

			IBurnable newBurnable = hit.transform.GetComponentInChildren<IBurnable>();

			if (newBurnable != null)
			{
				burnables.Add(newBurnable);
			}
			else { Debug.LogError("CacheBurnableNeightbors: Found burnable with no IBurnable: " + hit.transform.name); }
		}

		burnableNeighbors = burnables.ToArray();
		Debug.Log("Found " + burnableNeighbors.Length + " burnable neighbors");
	}

	private void UpdateVirulence()
	{
		if (isAblaze)
		{
			ChangeVirulence(virulenceGrowth, true);
		}
		else 
		{
			ChangeVirulence(propagationResistance, true);
		}
	}

	private void TryPropagation ()
	{
		propagationTimer += Time.deltaTime;

		if (propagationTimer >= propagationInterval)
		{
			if(RollPropagation())
			{
				PropagateFire();
			}
			/*
			else
			{
				Debug.Log("No propagation");
			}
			//*/

			propagationTimer -= propagationInterval;
		}
	}

	//can't propagate if unignited
	//propagation chance increases for more virulent fires
	private bool RollPropagation ()
	{
		if (!isAblaze) { return false; }

		//returns true if rolling a lower value than propagation chance
		return (Random.value * blazeIntensity) <= propagationChance;
	}

	private void PropagateFire ()
	{
		Debug.LogWarning(Random.value + " Propagating fire");

		//choose a random target among nearby neighbors
		IBurnable chosenTarget = burnableNeighbors[Random.Range(0, burnableNeighbors.Length)];

		chosenTarget.ChangeVirulence(propagationRate * blazeIntensity);
	}
//ENDOF private methods 
}
