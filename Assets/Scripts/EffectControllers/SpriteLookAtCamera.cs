using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookAtCamera : MonoBehaviour
{
    public void Update ()
    {
    	/*
    	transform.LookAt(
    		target: Camera.main.transform,
    		worldUp: Vector3.back
    	);
    	//*/
    	transform.forward = Camera.main.transform.forward;
    }
}
