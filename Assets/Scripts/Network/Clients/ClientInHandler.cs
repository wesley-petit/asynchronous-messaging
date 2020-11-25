using UnityEngine;

// Send request to the server to avoid a server 
// reference in a client side
public class ClientInHandler : MonoBehaviour
{
	[SerializeField] private Client _client = null;

	#region Unity Methods
	private void Start()
	{
		if (!_client)
		{
			Logger.Write($"Client is undefined in {name}", LogType.ERROR);
			return;
		}
	}

	private void OnEnable()
	{
		if (_client)
		{
			_client.OnSendRequest += SendRequests;
		}
	}

	private void OnDisable()
	{
		if (_client)
		{
			_client.OnSendRequest -= SendRequests;
		}
	}
	#endregion

	private void SendRequests()
	{
		if (_client.Request.Count <= 0)
			return;

		Logger.Write($"[{_client.OwnerClientId}] Send Requests");

		if (!_client)
			return;

		foreach (var clientRequest in _client.Request)
			ServerManager.Instance.AddRequest(new Request(clientRequest, _client));

		_client.Request.Clear();
	}
}
