using UnityEngine;

// Add request in client requests with the correct datas
// Take the server response and give it to other instance
public class ClientOutHandler : MonoBehaviour
{
	[SerializeField] private Client _client = null;
	[SerializeField] private float _timerMax = 10f;                 // Time limits where we send request
	[SerializeField] private Vector3 _playerPosition = Vector3.zero;

	private float _timer = 0f;

	#region Unity Methods
	private void Start()
	{
		if (!_client)
		{
			Logger.Write("Client is undefined", LogType.ERROR);
			return;
		}
	}

	private void Update()
	{
		_timer += Time.deltaTime;

		if (_timerMax < _timer)
		{
			_timer = 0f;

			if (!_client)
				return;

			foreach (var response in _client.Response)
				ReadResponse(response);

			_client.Response.Clear();

			_client.InvokeServerRpc(_client.SendRequest);
		}
	}
	#endregion

	#region Request And Response
	// With a specefic type, it will add the correct value in the client request
	public void AddTypeRequest(RequestType requestType)
	{
		if (!_client)
			return;

		Logger.Write($"[{_client.OwnerClientId}] Add Type Request {requestType}");
		string datas = "";

		switch (requestType)
		{
			case RequestType.SCAN_MESSAGES:
				datas = JsonUtility.ToJson(_playerPosition);
				break;

			default:
				Logger.Write($"[{_client.OwnerClientId}] Unknow request {requestType}", LogType.WARNING);
				break;
		}

		_client.AddRequest(new Request(requestType, datas));
	}

	private void ReadResponse(Request response)
	{
		RequestType requestType = response.RequestType;
		string datas = response.Datas;

		Logger.Write($"[{_client.OwnerClientId}] Response from Server of {requestType}");

		switch (requestType)
		{
			case RequestType.SCAN_MESSAGES:
				try
				{
					// Input
					Messages messages = JsonUtility.FromJson<Messages>(datas);

					// Give last value
				}
				catch (System.Exception e)
				{
					Logger.Write(e.ToString(), LogType.ERROR);
					return;
				}
				break;

			case RequestType.ADD_MESSAGE:
				break;

			default:
				Logger.Write($"Unknow response {requestType}", LogType.WARNING);
				break;
		}
	}
	#endregion
}