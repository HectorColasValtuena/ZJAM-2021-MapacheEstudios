using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiretruckPowerupPickup : PowerupBase
{
    protected override void DoPowerup ()
    {
    	FiretruckPowerupController.instance.Activate();
    }
}
