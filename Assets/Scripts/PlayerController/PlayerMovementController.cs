using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField]
	private float maxMoveSpeed = 1.0f; //maximum speed
	[SerializeField]
	private float maxAcceleration = 0.5f; //acceleration

	private Vector2 velocity;

	//acceleration for this frame
	private float currentAcceleration { get { return maxAcceleration * Time.deltaTime ; }} //* moveSpeedModifier; }}
	//speed for this frame
	private float currentMoveSpeed { get { return maxMoveSpeed * Time.deltaTime * moveSpeedModifier; }}
	private float moveSpeedModifier 
	{
			//if movement powerup return movement powerup speed
		get{ return (PowerupController.instance.turbo)? 2.0f : 1.0f; }
	}

	public void Awake ()
	{
		velocity = Vector2.zero;
	}

    public void Update ()
    {
        MovePlayer(InputController.instance.direction);
    }

    private void MovePlayer (Vector2 direction)
    {
    	velocity = Vector2.MoveTowards(
    		current: velocity,
    		target: direction,
    		maxDistanceDelta: currentAcceleration
    	);

    	Vector2 movementVector = velocity * currentMoveSpeed;

    	transform.Translate(new Vector3(x: movementVector.x, y: 0, z: movementVector.y), Space.World);
    }
}
