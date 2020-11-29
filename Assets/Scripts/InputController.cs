using UnityEngine;

public enum ButtonState
{
	NO_PRESS,
	UP,
	DOWN
}

public class InputController : MonoBehaviour
{
	[SerializeField] private float _deadZone = 0.01f;

	#region Public Fields
	public Vector2 Axis { get; private set; }
	public Vector2 RawAxis { get; private set; }
	public Vector2 Mouse { get; private set; }
	public bool Jump { get; private set; }
	public ButtonState CrouchState { get; private set; } = ButtonState.NO_PRESS;
	public bool DeadZoneAxis => Axis.magnitude <= _deadZone;
	public bool DeadZoneMouse => Mouse.magnitude <= _deadZone;
	#endregion

	#region Unity Methods
	private void Update()
	{
		Axis = new Vector2(Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical"));

		RawAxis = new Vector2(Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical"));

		Mouse = new Vector2(Input.GetAxis("Mouse X"),
			Input.GetAxis("Mouse Y"));

		Jump = Input.GetButtonDown("Jump");

		//if (Input.GetButtonDown("Crouch"))
		//{
		//	CrouchState = ButtonState.DOWN;
		//}
		//else if (Input.GetButtonUp("Crouch"))
		//{
		//	CrouchState = ButtonState.UP;
		//}
		//else
		//{
		//	CrouchState = ButtonState.NO_PRESS;
		//}
	}
	#endregion
}