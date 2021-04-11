using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTurretFrontal : MonoBehaviour
{
	[SerializeField]
	private float shotInterval = 0.1f;
	[SerializeField]
	private GameObject shotPrefab;
	[SerializeField]
	private Transform barrel;

    private float shotTimer = 0.0f;

    public void Update ()
    {
    	shotTimer += Time.deltaTime;
    	if (shotTimer < shotInterval) { return; }
    	Instantiate(shotPrefab, barrel.position, barrel.rotation);
        shotTimer = 0;
    }
}
