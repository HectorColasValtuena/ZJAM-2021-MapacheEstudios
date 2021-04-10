using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDumperController : MonoBehaviour
{
//private variables
	[SerializeField]
	private GameObject shotPrefab;
	[SerializeField]
	private Transform shotOrigin;

	//[SerializeField]
	private int maximumCharges = 200;	//maximum water charge
	//[SerializeField]
	private float shotInterval = 1/6;	//Time between shots

	private float shotTimer = 0;
	private bool shotReady { get { return shotTimer >= shotInterval; }}
	private bool shotInput { get { return InputController.instance.dumpWater; }}
//ENDOF private variables

//MonoBehaviour lifecycle
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
		Instantiate(
			original: shotPrefab,
			position: shotOrigin.position,
			rotation: Quaternion.identity	//Random.rotation
		);
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
