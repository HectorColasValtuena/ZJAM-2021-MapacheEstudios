using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBuilding :
	BurnableBuilding,
	IDestructable
{
//Constants
	protected const string animation_burnt = "burnt";
	protected const string animation_exploded = "Destroyed";
	private const float burntThreshold = 0.5f;
//ENDOF Constants

//protected fields
	[SerializeField]
	protected float hitPoints = 20f;
	[SerializeField]
	protected float explosionVirulence = 0.5f;
	[SerializeField]
	protected int explosionTargetCount = 3;
	[SerializeField]
	protected float explosionRadius = 2f;
	[SerializeField]
	protected float burntVirulenceRatio = 0.5f;
	[SerializeField]
	protected float burntPropagationRatio = 0.5f;
//ENDOF protected fields

//IDestructable
	public bool isDestroyed { get { return exploded; }}
//ENDOF IDestructable

//private fields
	[SerializeField]
	private GameObject explosionPrefab;

	private bool isBurnt = false;

	private bool exploded = false;
//ENDOF private fields

	public override void Update ()
	{
		base.Update();
		UpdateHitPoints();
		UpdateScore();
	}

//private methods
	private void UpdateHitPoints ()
	{
		hitPoints -= Mathf.Max(fireVirulence, 0f) * Time.deltaTime;

		if (!isBurnt  && hitPoints <= (hitPoints * burntThreshold))
		{

		}

		if (!exploded && hitPoints <= 0f)
		{
			exploded = true;
			Explode();
		}
	}

	private void Explode ()
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);

		animator.SetBool(animation_exploded, true);

		IBurnable[] burnables = BurnableCache.FindBurnablesAroundPoint(transform.position, explosionRadius);

		for (int i = 0; i < explosionTargetCount; i++)
		{
			Ignite(burnables[Random.Range(0, burnables.Length)]);
		}

		ExplosionAttributeChange ();
	}

	private void ExplosionAttributeChange ()
	{
		propagationChance *= burntPropagationRatio;
		propagationRate *= burntPropagationRatio;
		propagationDistance *= burntPropagationRatio;

		virulenceGrowth *= burntVirulenceRatio;
		maximumVirulence *= burntVirulenceRatio;
	}

	private void Ignite (IBurnable target)
	{ target.ChangeVirulence(explosionVirulence); }

    private IEnumerator ScoringLoop ()
    {
    	yield return new WaitForSeconds(1f);
    	while (!isDestroyed)
    	{
    		yield return new WaitForSeconds(1f);
    		UpdateScore();
    	}
    }

    private const float scoreInterval = 1f;
    private float scoreTimer = 0;
    private void UpdateScore ()
    {
    	if (isDestroyed) { return; }
    	scoreTimer += Time.deltaTime;
    	if (scoreTimer >= scoreInterval)
    	{
    		scoreTimer -= scoreInterval;
    		ScoreCounter.ScoreGain();
    	}
    }
//private methods
}
