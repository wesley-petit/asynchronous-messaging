using UnityEngine;

// Send request to the server to avoid a server 
// reference in a client side
public class ClientSender : MonoBehaviour
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
			Logger.Write($"[{_client.OwnerClientId}] Send Requests");

			foreach (var clientRequest in _client.Request)
				SendRequest(clientRequest);

			_client.Request.Clear();
		}
	}
	#endregion

	private void SendRequest(Request clientRequest) => ServerManager.Instance.AddRequest(new Request(clientRequest, _client));
}
