using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTurbo : PowerupBase
{
    protected override void DoPowerup ()
    {
    	PowerupController.instance.turbo = true;
    }
}
