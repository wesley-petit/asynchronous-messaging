using UnityEngine;

[RequireComponent(typeof(InputController))]
public class FPSLook : MonoBehaviour
{
	[SerializeField, Range(0f, 200f)] private float _mouseSensitivity = 100f;
	[SerializeField] private Transform _playerBody = null;
	[SerializeField] private Transform _cameraTransform = null;
	[SerializeField] private float _limitCameraRotation = 90f;
	[SerializeField] private CursorLockMode _cursorMode = CursorLockMode.Locked;

	private InputController _inputs;
	private float _xRotation = 0f;

	#region Unity Methods
	private void Start()
	{
		_inputs = GetComponent<InputController>();
		Cursor.lockState = _cursorMode;
	}

	private void Update() => LookAt();
	#endregion

	private void LookAt()
	{
		if (_inputs.IsMouseEmpty)
			return;

		// Inputs
		var mouse = _inputs.Mouse;
		//mouse.Normalize();
		mouse *= _mouseSensitivity * Time.deltaTime;

		// Initialistion rotation camera
		_xRotation -= mouse.y;
		_xRotation = Mathf.Clamp(_xRotation, -_limitCameraRotation, _limitCameraRotation);

		_cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
		_playerBody.Rotate(Vector3.up * mouse.x);
	}
}