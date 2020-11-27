using UnityEngine;

// Add request in client requests with the correct datas
// Read server response and give it to other instance
public class ClientOutHandler : MonoBehaviour
{
	[SerializeField] private Client _client = null;

	// Value
	[SerializeField] private Vector3 _playerPosition = Vector3.zero;
	[SerializeField] private Message[] _messages = new Message[0];
	[SerializeField] private string[] _messagesPremades = new string[0];
	[SerializeField] private Ping _ping = new Ping();

	#region Unity Methods
	private void Start()
	{
		if (!_client)
		{
			Logger.Write("Client is undefined", LogType.ERROR);
			return;
		}

		AddRequest(RequestType.MESSAGE_PREMADE, "Fill blank to avoid the empty verification");

		// TODO Remove it
		//AddRequest(RequestType.SCAN_MESSAGES, _playerPosition);
		//AddRequest(RequestType.ADD_MESSAGE, new Message("RaskasseVolante", "Add Message", System.DateTime.Now.ToString(), new Vector3(5f, 5f)));
		//AddRequest(RequestType.PING, new Ping());
	}

	private void Update()
	{
		if (!_client)
			return;

		if (0 < _client.Response.Count)
		{
			foreach (var response in _client.Response)
				ReadResponse(response);

			_client.Response.Clear();
		}
	}
	#endregion

	#region Request And Response

	// Generic add request
	public void AddRequest<T>(RequestType requestType, T requestDatas)
	{
		if (!_client)
			return;

		Logger.Write($"[{_client.OwnerClientId}] Add Request {requestType}");
		string datas = JsonUtility.ToJson(requestDatas);

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
				// Input
				Messages messages = JsonUtility.FromJson<Messages>(datas);
				_messages = messages.GetMessages;
				break;

			case RequestType.MESSAGE_PREMADE:
				// Input
				MessagesPremades messagesPremades = JsonUtility.FromJson<MessagesPremades>(datas);
				_messagesPremades = messagesPremades.GetMessagesPremades;
				break;

			case RequestType.PING:
				// Input
				_ping = JsonUtility.FromJson<Ping>(datas);
				_ping.CalcultatePing();
				break;

			default:
				Logger.Write($"Unknow response {requestType}", LogType.WARNING);
				break;
		}
	}
	#endregion
}