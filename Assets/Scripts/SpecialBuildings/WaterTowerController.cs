using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTowerController : MonoBehaviour
{
//Constants
	private const string animation_bool_waterFull = "WaterFull";
	private const string animation_trigger_used = "Used";
	private const float reloadTime = 5f;
//ENDOF Constants

//static space
	private bool freeReload { get { return PowerupController.instance.water; }}
//ENDOF static space

//private variables
	private float timer = 0;
	private Animator animator;
//ENDOF private variables

//private properties
	private bool isFull { get { return timer >= reloadTime; }}
//ENDOF private properties

//MonoBehaviour lifecycle
	public void Awake ()
	{
		animator = GetComponent<Animator>();

		timer = reloadTime;
	}

	public void Update ()
	{
		UpdateReloadTimer();
	}

	public void OnTriggerEnter (Collider collider)
	{
		Debug.Log("TriggerEnter");
		IWaterDumperController waterDumper = collider.GetComponent<IWaterDumperController>();
		if (waterDumper == null || !isFull) { return; }
		Used();
		waterDumper.Reload();
	}
//ENDOF MonoBehaviour lifecycle 

//private methods
	private void UpdateReloadTimer ()
	{
		if (freeReload) { timer = reloadTime; }

		timer += Time.deltaTime;

		if (isFull) { animator.SetBool(animation_bool_waterFull, true); }
	}

	private void Used ()
	{
		timer = 0;
		animator.SetTrigger(animation_trigger_used);
		animator.SetBool(animation_bool_waterFull, false);
	}
//ENDOF private methods 
}
