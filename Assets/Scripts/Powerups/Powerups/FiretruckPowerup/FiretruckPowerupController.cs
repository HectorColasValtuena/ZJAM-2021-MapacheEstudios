using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiretruckPowerupController : MonoBehaviour
{

    public static FiretruckPowerupController instance;

    [SerializeField]
    private GameObject truckPrefab;

    [SerializeField]
    private Transform[] truckSpawners;

//MonoBehaviour
    public void Awake ()
    {
    	instance = this;
    }
//ENDOF


    public void Activate()
    {
    	foreach (Transform spawner in truckSpawners)
    	{
    		Instantiate(truckPrefab, spawner.position, spawner.rotation);
    	}
    }
}
