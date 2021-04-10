using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDumperController : MonoBehaviour
{
//Constants
	private const string animatorTrigger_WaterEmpty = "WaterEmpty";

	//[SerializeField]
	private const int maximumCharges = 30;	//maximum water charge
	//[SerializeField]
	private const float shotInterval = 1/6;	//Time between shots
//ENDOF Constants


//private variables
	private Animator animator;

	[SerializeField]
	private GameObject shotPrefab;
	[SerializeField]
	private Transform shotOrigin;


	private float shotTimer = 0;
	private bool shotReady { get { return shotTimer >= shotInterval; }}
	private bool shotInput { get { return InputController.instance.dumpWater; }}

	private int availableCharges
	{
		get { return _availableCharges; }
		set { _availableCharges = Mathf.Clamp(value, 0, maximumCharges); }
	}
	[SerializeField]
	private int _availableCharges;
//ENDOF private variables

//MonoBehaviour lifecycle
	public void Awake ()
	{
		animator = GetComponent<Animator>();

		availableCharges = maximumCharges;
	}

	public void Update ()
	{
		UpdateShooting();
	}
//ENDOF MonoBehaviour lifecycle

//private methods
	private void UpdateShooting ()
	{
		if (shotReady && shotInput)
		{
			Shoot();
		}
		else
		{
			CountdownShotTimer();
		}
	}

	private void Shoot ()
	{
		if (availableCharges > 0)
		{
			Instantiate(
				original: shotPrefab,
				position: shotOrigin.position,
				rotation: Quaternion.identity	//Random.rotation
			);
			availableCharges--;
		}
		else {
			animator.SetTrigger(animatorTrigger_WaterEmpty);
		}
	}

	private void CountdownShotTimer ()
	{
		if (shotTimer < shotInterval)
		{
			shotTimer += Time.deltaTime;
		}
		else 
		{
			shotTimer = shotInterval;
		}
	}
//ENDOF private methods
}
