using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialForce : MonoBehaviour
{
	public Vector3 force;
	public Vector3 randomForce;

    public void Awake()
    {
    	Rigidbody rb = gameObject.GetComponent<Rigidbody>();

    	Vector3 directedForce = transform.rotation * (force + (randomForce * Random.Range(0f, 1f)));
    	rb.AddForce(directedForce);
    }
}
