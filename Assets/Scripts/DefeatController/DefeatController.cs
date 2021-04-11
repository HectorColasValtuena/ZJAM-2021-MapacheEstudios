using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatController : MonoBehaviour
{
	public DestructableBuilding headquarters;

	public LerpFollowController cameraController;

	private bool defeated = false;

	    // Update is called once per frame
    void Update()
    {
 		if (!defeated && headquarters.isDestroyed)
 		{
 			defeated = true;
 			Defeat();
 		}
    }

    private void Defeat ()
    {
    	Debug.LogWarning("DEFEATED");
    	cameraController.followTarget = headquarters.transform;
    }
}
