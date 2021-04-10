using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterModelRotation : MonoBehaviour
{
//Constants
	private const float lerpRate = 0.05f;
//ENDOF Constants

//private variables
	private Vector3 oldPosition;

	[SerializeField]
	private Transform rootTransform;
//ENDOF private variables

//private properties
	private Quaternion rotation
	{
		get
		{
			return rootTransform.rotation;
		}
		set
		{
			rootTransform.rotation = value;
		}
	}

	private Vector3 currentPosition
	{
		get
		{
			return rootTransform.position;
		}
		set
		{
			rootTransform.position = value;
		}
	}

	private Quaternion desiredRotation
	{
		get
		{
			if (currentPosition == oldPosition)
			{
				return rotation;
			}
			return Quaternion.LookRotation(currentPosition - oldPosition);
		}
	}
//ENDOF private properties

//MonoBehaviour lifecycle
	public void Awake ()
	{
		if (rootTransform == null) { Debug.LogError("HelicopterModelRotation missing rootTransform: " + gameObject.name); }
		oldPosition = currentPosition;
	}

	public void Update ()
	{
		UpdateRotation();
	}
//ENDOF MonoBehaviour lifecycle

//private methods
	private void UpdateRotation ()
	{
		rotation = Quaternion.Lerp(rotation, desiredRotation, lerpRate);
		oldPosition = currentPosition;
	}
//ENDOF private methods 
}