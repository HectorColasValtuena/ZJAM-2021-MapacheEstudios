using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
	private const float scoringInterval = 1.0f;
	public static ulong score
	{
		get {return _score;}
		private set { if (!DefeatController.defeated) { _score = value; }}
	}
   private static ulong _score = 0;
    public static void ScoreGain (int gain = 1)
    {
    	score += (ulong) gain;
    }

    private IDestructable[] destructables;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ScoringLoop());
    }

    private IEnumerator ScoringLoop ()
    {
    	while (!DefeatController.defeated)
    	{
    		yield return new WaitForSeconds(1f);
    		UpdateScore();
    	}
    }

    private void UpdateScore ()
    {

    }
}
