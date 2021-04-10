using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSelfDestruct : MonoBehaviour
{
	[SerializeField]
	private float time = 5f;

	private float timer = 0f;

	public void Update ()
	{
		timer += Time.deltaTime;

		if (timer > time)
		{
			Destroy(gameObject);
		}
	}
}
