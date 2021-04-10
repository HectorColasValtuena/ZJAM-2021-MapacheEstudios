using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShotController : MonoBehaviour
{
//constants
	private const float minYPosition = -0.5f;
//ENDOF constants

//private fields
	[SerializeField]
	private GameObject hitEffectPrefab;

	[SerializeField]
	private float dousingPower = 0.4f;
//ENDOF private fields

//MonoBehaviour lifecycle
	public void Update ()
	{
		CheckOutOfBounds();
	}

	public void OnTriggerEnter (Collider collider)
	{
		Debug.Log("Trigger Enter");
		IBurnable burnable = collider.GetComponent<IBurnable>();
		Hit(burnable);
	}
//ENDOF MonoBehaviour lifecycle

//private methods
	private void CheckOutOfBounds ()
	{
		if (transform.position.y <= minYPosition)
		{
			Destroy(gameObject);
		}
	}

	private void Hit (IBurnable burnable)
	{
		if (burnable == null) 
		{
			Debug.LogError("waterShot hit didn't find IBurnable");
			return;
		}

Debug.LogWarning("WaterHit");

		burnable.ChangeVirulence(dousingPower);

		if (hitEffectPrefab != null)
		{
			Instantiate(
				original: hitEffectPrefab,
				position: transform.position,
				rotation: Quaternion.identity	//Random.rotation
			);
		}

		Destroy(gameObject);
	}
//ENDOF private methods
}
