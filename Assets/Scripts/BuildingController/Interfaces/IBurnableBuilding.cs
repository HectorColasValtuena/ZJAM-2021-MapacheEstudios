using UnityEngine;

public interface IBurnableBuilding
{
	bool isAblaze {get;}	//True if building is in flames

	float blazeIntensity {get;} //0 to 1 representation of how intense flames are

	void ChangeVirulence (float fireVirulence, bool ignoreDeltaTime = false);	//changes intensity of the flames
}
