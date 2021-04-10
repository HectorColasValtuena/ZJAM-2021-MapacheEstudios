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
	private float dousingRadius = 2f;
	[SerializeField]
	private float dousingPower = -0.6f;

	
	[SerializeField]
	private LayerMask collidableLayerMask;

	[SerializeField]
	private GameObject hitEffectPrefab;

//ENDOF private fields

//MonoBehaviour lifecycle
	public void Update ()
	{
		CheckOutOfBounds();
	}

	/*
	public void OnTriggerEnter (Collider collider)
	{
		Debug.Log("Trigger Enter");
		IBurnable burnable = collider.GetComponent<IBurnable>();
		Hit(burnable);
	}
	//*/
	public void OnCollisionEnter (Collision collision)
	{
		Hit(collision.GetContact(0).point);
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

	private void Hit (Vector3 position)
	{
		RaycastHit[] hits = Physics.SphereCastAll(
			origin: position,
			radius: dousingRadius,
			direction: Vector3.up,
			maxDistance: dousingRadius,
			layerMask: collidableLayerMask
			//queryTriggerInteraction: QueryTriggerInteraction.Collide
		);

		
		foreach (RaycastHit hit in hits)
		{
			//if (hit.transform == this.transform) { continue; } //ignore oneself

			IBurnable burnable = hit.transform.GetComponentInChildren<IBurnable>();

			if (burnable != null)
			{
				Douse(burnable);
			}
		}

		if (hitEffectPrefab != null)
		{
			Instantiate(
				original: hitEffectPrefab,
				position: position,
				rotation: Quaternion.identity	//Random.rotation
			);
		}

		Destroy(gameObject);
	}

	private void Douse (IBurnable burnable)
	{
		burnable.ChangeVirulence(dousingPower);

	}
//ENDOF private methods
}
