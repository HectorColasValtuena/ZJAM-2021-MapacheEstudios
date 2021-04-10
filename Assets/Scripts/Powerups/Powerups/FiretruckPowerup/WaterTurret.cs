using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTurret : MonoBehaviour
{
	[SerializeField]
	private float shotInterval = 0.1f;
	[SerializeField]
	private GameObject shotPrefab;
	[SerializeField]
	private Transform barrel;

    public Transform target = null;

    private float shotTimer = 0.0f;

    public void Update ()
    {
    	shotTimer += Time.deltaTime;
    	if (target == null || shotTimer < shotInterval) { return; }
    	ShootAt(target);
    }

    private void ShootAt (Transform target)
    {
    	transform.LookAt(target);
    	Instantiate(shotPrefab, barrel.position, barrel.rotation);
    }
}
