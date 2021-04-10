using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiretruckMovementController : MonoBehaviour
{
    [SerializeField]
    private float velocity;

    public void Update ()
    {
    	transform.Translate(0, 0, velocity * Time.deltaTime);
    }
}
