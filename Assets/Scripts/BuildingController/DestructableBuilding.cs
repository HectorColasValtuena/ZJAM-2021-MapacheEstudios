using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBuilding :
	BurnableBuilding,
	IDestructableBuilding
{
//IDestructableBuilding
	public	bool isDestroyed { get { return false; }}
//ENDOF IDestructableBuilding
}
