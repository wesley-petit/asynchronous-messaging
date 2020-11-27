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

	private void Update()
	{
		if (!_client)
			return;

		if (0 < _client.Request.Count)
		{
			SendRequests();
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
