using UnityEngine;

public class LerpFollowController : MonoBehaviour
{
	[SerializeField]
	public Transform followTarget;

	private float lerpRate = 0.05f;

    public void Update ()
    {
    	transform.position = Vector3.Lerp(transform.position, followTarget.position, lerpRate);
    }
}
