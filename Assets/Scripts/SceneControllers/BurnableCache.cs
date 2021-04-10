using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableCache : MonoBehaviour
{
//    public static BurnableCache instance;

    public static IBurnable[] sceneBurnables;
    public static IBurnable randomBurnable { get { return sceneBurnables[Random.Range(0, sceneBurnables.Length)]; }}

    public void Awake ()
    {
		sceneBurnables = Object.FindObjectsOfType<BurnableBuilding>();
		Debug.Log("Found " + sceneBurnables.Length + " burnable neighbors");

    }
}
