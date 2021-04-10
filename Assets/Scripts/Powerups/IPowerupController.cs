using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerupController
{
    bool turbo {get; set;}

    bool water {get; set;}

    bool drones {set;}
    
    bool ultimate {set;}
}
