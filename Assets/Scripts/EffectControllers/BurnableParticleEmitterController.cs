using UnityEngine;

public class BurnableParticleEmitterController : MonoBehaviour
{
//private variables
	[SerializeField]
	private float maximumEmissionRate = 20f;

	[SerializeField]
	private Transform burnableTransform;

	private IBurnable burnable;

	[SerializeField]
	private ParticleSystem particleEmitter;
	private ParticleSystem.EmissionModule emissionController;
//ENDOF private variables

//private fields
	private float desiredEmissionRate { get { return maximumEmissionRate * burnable.blazeIntensity; }}
//ENDOF private fields

//MonoBehaviour lifecycle
	public void Awake ()
	{
		burnable = burnableTransform.GetComponent<IBurnable>();
		if (burnable == null) { Debug.LogError("BurnableParticleEmitter missing burnable: " + gameObject.name); }

		particleEmitter = transform.GetComponent<ParticleSystem>();
		if (particleEmitter == null) { Debug.LogError("BurnableParticleEmitter missing particleSystem: " + gameObject.name); }
		emissionController = particleEmitter.emission;
	}

	public void Update ()
	{
		UpdateParticleEmitter();
	}
//ENDOF MonoBehaviour lifecycle

//private methods
	private void UpdateParticleEmitter ()
	{
		emissionController.rateOverTimeMultiplier = desiredEmissionRate;
	}
//ENDOF private methods 

}
