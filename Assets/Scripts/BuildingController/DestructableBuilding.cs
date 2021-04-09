using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBuilding :
	BurnableBuilding,
	IDestructable
{
//IDestructable
	public bool isDestroyed { get { return false; }}
//ENDOF IDestructable
}
