using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(CharacterController))]
public class FPSMovement : MonoBehaviour
{
	[SerializeField, Range(0, 30f)] private float speed = 5f;

	[Header("Gravity And Jump")]
	[SerializeField] private float _gravity = -9.81f;
	[SerializeField, Range(0f, 30f)] private float _jumpHeight = 3f;
	[SerializeField] private Transform _groundCheck = null;
	[SerializeField] private float _radius = 0.4f;
	[SerializeField] private LayerMask _groundMask = new LayerMask();
	[SerializeField] private bool _isGrounded;

	private InputController _inputs;
	private CharacterController _characterController;
	private Vector3 _velocity = Vector3.zero;

	#region Unity Methods
	private void Start()
	{
		_inputs = GetComponent<InputController>();
		_characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		Move();
		Jump();
		ApplyGravity();
	}

	private void OnValidate() => _gravity = Physics.gravity.y;
	#endregion

	private void Move()
	{
		if (_inputs.IsMoveEmpty)
			return;

		var input = _inputs.Movement;
		Vector3 move = transform.right * input.x + transform.forward * input.y;

		_characterController.Move(move * speed * Time.deltaTime);
	}

	private void Jump()
	{
		if (!_inputs.Jump || !_isGrounded)
			return;

		_velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity) * Time.deltaTime;
	}

	private void ApplyGravity()
	{
		_velocity.y += _gravity * Mathf.Pow(Time.deltaTime, 2);
		_characterController.Move(_velocity);

		_isGrounded = Physics.CheckSphere(_groundCheck.position, _radius, _groundMask);

		if (_isGrounded && _velocity.y < 0)
			_velocity.y = -2f;
	}
}
