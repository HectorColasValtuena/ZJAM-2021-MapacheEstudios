using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDumperController :
	MonoBehaviour,
	IWaterDumperController
{
//Constants
	private const string animatorTrigger_WaterEmpty = "WaterEmpty";

	//[SerializeField]
	private const int maximumCharges = 6;//180;	//maximum water charge
	//[SerializeField]
	private const float shotInterval = .125f;	//Time between shots
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

	private bool freeReload { get { return PowerupController.instance.water; }}
//ENDOF private variables

//Interface implementation
	void IWaterDumperController.Reload ()	//reloads water reservoir
	{
		this.Reload();
	}
//ENDOF Interface implementation

//MonoBehaviour lifecycle
	public void Awake ()
	{
		animator = GetComponent<Animator>();

		Reload();
	}

	public void Update ()
	{
		UpdateShooting();
		if (freeReload) { Reload(); }
	}
//ENDOF MonoBehaviour lifecycle

//private methods
	private void Reload ()
	{
		availableCharges = maximumCharges;
	}

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
			shotTimer = 0;// shotInterval;
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
