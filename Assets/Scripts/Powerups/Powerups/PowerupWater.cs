using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupWater : PowerupBase
{
    protected override void DoPowerup ()
    {
    	PowerupController.instance.water = true;
    }
}
