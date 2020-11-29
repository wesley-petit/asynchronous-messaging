using UnityEngine;

public class InputController : MonoBehaviour
{
	#region Public Fields
	public Vector2 Movement { get; private set; }
	public Vector2 Mouse { get; private set; }
	public bool Jump { get; private set; }
	public bool IsMoveEmpty => Movement == Vector2.zero;
	public bool IsMouseEmpty => Mouse == Vector2.zero;
	#endregion

	#region Unity Methods
	private void Update()
	{
		Movement = new Vector2(Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical"));

		Mouse = new Vector2(Input.GetAxis("Mouse X"),
			Input.GetAxis("Mouse Y"));

		Jump = Input.GetButtonDown("Jump");
	}
	#endregion
}