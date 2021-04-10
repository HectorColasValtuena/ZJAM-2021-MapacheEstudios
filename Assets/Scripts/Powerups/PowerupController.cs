using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour, IPowerupController
{
//Constants
	private const float turboLength = 20f;
	private const float waterLength = 20f;
//ENDOF Constants

	public static IPowerupController instance;

//IPowerupController
    public bool turbo {
    	get
    	{
    		return turboTimer > 0;
    	}
    	set
    	{
    		turboTimer = (value) ? turboLength : 0;
    	}
    }
    public bool water {
    	get
    	{
    		return waterTimer > 0;
    	}
    	set
    	{
    		waterTimer = (value) ? waterLength : 0;
    	}
    }
    public bool drones {get; set;}
    
    public bool ultimate {get; set;}
//ENDOF IPowerupController


//private variables
    private float turboTimer = 0;
    private float waterTimer = 0;
//ENDOF private variables

//MonoBehaviour lifecycle
    public void Awake ()
    {
    	instance = this;
    }

    public void Update ()
    {
    	UpdateTimers();
    }
//ENDOF MonoBehaviour lifecycle

//private methods
    private void UpdateTimers ()
    {
    	turboTimer -= Time.deltaTime;
    	waterTimer -= Time.deltaTime;
    }
//ENDOF
}
