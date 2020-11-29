using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(CharacterController))]
public class TPSMovement : MonoBehaviour
{
	[SerializeField, Range(0, 30f)] private float _speed = 5f;
	[SerializeField] private float _turnSmoothTime = 0.1f;

	[Header("Camera Relative")]
	[SerializeField] private bool _movementRelativeWithCamera = true;
	[SerializeField] private Transform _cameraTransform = null;

	private InputController _inputs;
	private CharacterController _characterController;
	private float _turnSmoothVelocity;

	#region Unity Methods
	private void Start()
	{
		_inputs = GetComponent<InputController>();
		_characterController = GetComponent<CharacterController>();
	}

	private void Update() => Move();
	#endregion Unity Methods

	private void Move()
	{
		if (_inputs.DeadZoneAxis)
			return;

		var axis = _inputs.Axis;
		Vector3 direction = new Vector3(axis.x, 0f, axis.y).normalized;

		// Rotation et mouvement dépendant de la position de la camera
		if (_movementRelativeWithCamera)
		{
			direction = RotateRelativeWithCamera(direction);
		}
		else
		{
			BasicRotate(direction);
		}

		_characterController.Move(direction.normalized * _speed * Time.deltaTime);
	}

	// Transforme l'axis en angle pour la rotation
	#region Rotate
	private void BasicRotate(Vector3 direction)
	{
		if (0.1f <= direction.magnitude)
		{
			float targetAngle = GetBasicTargetAngle(direction);
			float angle = SmoothAngle(targetAngle);

			transform.rotation = Quaternion.Euler(0f, angle, 0f);
		}
	}

	private Vector3 RotateRelativeWithCamera(Vector3 direction)
	{
		if (0.1f <= direction.magnitude)
		{
			float targetAngle = GetBasicTargetAngle(direction) + _cameraTransform.eulerAngles.y;
			float angle = SmoothAngle(targetAngle);

			transform.rotation = Quaternion.Euler(0f, angle, 0f);

			// Déplacement du joueur dépend de la postion de la caméra
			direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
		}

		return direction;
	}

	private float GetBasicTargetAngle(Vector3 direction) => Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

	private float SmoothAngle(float targetAngle)
	{
		return Mathf.SmoothDampAngle(transform.eulerAngles.y,
			targetAngle,
			ref _turnSmoothVelocity,
			_turnSmoothTime);
	}
	#endregion Rotate
}