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
	public void ChangeVirulence (float fireVirulenceChange, bool ignoreDeltaTime = false)
	{
		//updates virulence appling time delta if not ignored
		fireVirulence = Mathf.Clamp(
			value: fireVirulence + (fireVirulenceChange * ((ignoreDeltaTime) ? 1.0f : Time.deltaTime)),
			min: propagationResistance,
			max: maximumVirulence
		);
	}
//ENDOF IBurnable implementation

//protected hierarchy values
  //Serialized values
	[SerializeField]
	protected float propagationResistance = -0.1f;
	[SerializeField]
	protected float propagationChance = 0.1f;
	[SerializeField]
	protected float propagationRate = 0.1f;
	[SerializeField]
	protected float propagationDistance = 3.0f;
	[SerializeField]
	protected float virulenceGrowth = 0.1f;
	[SerializeField]
	protected float maximumVirulence = 1f;
  //ENDOF Serialized values

	protected Animator animator;

	[SerializeField]
	protected float fireVirulence = 0.0f;
//ENDOF protected hierarchy values

//private variables
	private float propagationTimer = 0.0f;

//ENDOF private variables


//MonoBehaviour lifecycle
	public void Awake ()
	{
		animator = GetComponent<Animator>();
		fireVirulence = propagationResistance;
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
	private void UpdateVirulence()
	{
		if (isAblaze)
		{
			ChangeVirulence(virulenceGrowth);
		}
		else 
		{
			ChangeVirulence(propagationResistance);
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
			else
			{
				Debug.Log("No propagation");
			}

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
	}
//ENDOF private methods 
}
