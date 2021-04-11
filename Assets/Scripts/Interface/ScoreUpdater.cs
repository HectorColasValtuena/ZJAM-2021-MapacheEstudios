using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    public UnityEngine.UI.Text scoreText;

    // Update is called once per frame
    public void Update()
    {
        scoreText.text = "" + ScoreCounter.score;
    }
}
