using UnityEngine;
using MLAPI.NetworkedVar;
using MLAPI;

[System.Serializable]
public class ClientData : NetworkedBehaviour
{
	private NetworkedVar<Vector3> _playerPosition =
		new NetworkedVar<Vector3>(NetworkedSettings.ServerRead, Vector3.zero);

	// TODO Delete it after development
	[Tooltip("Show player position, doesn't modify it")]
	public Vector3 displayPlayer = Vector3.zero;

	#region Getter And Setter
	public Vector3 PlayerPosition
	{
		get => _playerPosition.Value;
		set => _playerPosition.Value = value;
	}
	#endregion

	private void Update()
	{
		displayPlayer = PlayerPosition;
	}
}
