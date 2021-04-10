using UnityEngine;

public abstract class PowerupBase : MonoBehaviour, IPowerup
{
	public GameObject particlePrefab;

//IPowerup
	public void Activate ()
	{
		DoPowerup();

		if (particlePrefab != null)
		{
			Instantiate(particlePrefab, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}
//ENDOF

public void OnTriggerEnter (Collider collider)
{
	Debug.Log("Powerup TriggerEnter");
	Activate();
}

//protected abstract
	protected abstract void DoPowerup ();


}
