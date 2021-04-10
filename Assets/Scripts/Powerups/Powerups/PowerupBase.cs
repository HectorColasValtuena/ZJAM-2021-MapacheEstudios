using UnityEngine;

public abstract class PowerupBase : MonoBehaviour, IPowerup
{
	public GameObject particlePrefab;
	public GameObject explosionPrefab;

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

	public void Destroy ()
	{
		if (explosionPrefab != null)
		{
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
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
