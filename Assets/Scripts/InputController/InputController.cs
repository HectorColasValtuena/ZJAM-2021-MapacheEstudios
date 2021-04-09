using UnityEngine;

public class InputController : IInputController
{
    public static IInputController instance
    {
    	get {
    		if (_instance == null)
    		{
    			_instance = new InputController();
    		}
    		return _instance;
    	}
    }
    private static IInputController _instance;

//IInputController implementation
	public Vector2 direction
	{
		get
		{
			return new Vector2(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical")
			);
		}
	}

	public bool dumpWater
	{
		get
		{
			return Input.GetKey(KeyCode.Space);
		}
	}
//ENDOF IInputController implementation
}
