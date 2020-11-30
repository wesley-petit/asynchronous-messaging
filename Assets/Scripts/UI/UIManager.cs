using UnityEngine;

// Use by the client to send request
// Add request in client requests with the correct datas
// Invoke callbacks when a specific response arrives
public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; } = null;

	#region Callbacks
	public System.Action<string[]> OnMessagePremade = null;
	public System.Action<Message[]> OnScanMessage = null;
	public System.Action<string> OnPing = null;
	#endregion
	public string UserName { get; set; } = "";

	private Client _client = null;

	// Singleton
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Logger.Write($"There is two Singleton of the same type : {typeof(UIManager)}", LogType.ERROR);
			Destroy(gameObject);
		}
	}

	#region Invoke Callbacks
	public void SetMessagePremade(string[] messagesPremades) => OnMessagePremade?.Invoke(messagesPremades);

	public void SetMessageArrival(Message[] messages) => OnScanMessage?.Invoke(messages);

	public void SetPing(string pingValue) => OnPing?.Invoke(pingValue);
	#endregion

	public void Register(Client client)
	{
		if (!client)
			return;

		_client = client;
		SendMessagesPremades();
	}

	public void Unregister() => _client = null;

	#region Wraper Send Request
	public void SendScanMessage(Vector3 position) => AddRequest(RequestType.SCAN_MESSAGES, position);

	public void SendAddMessage(string content, Vector3 position)
	{
		AddRequest(RequestType.ADD_MESSAGE, new Message(UserName, content, System.DateTime.Now.ToString(), position));

		// Scan after adding a message
		SendScanMessage(position);
	}

	public void SendPing() => AddRequest(RequestType.PING, new Ping());

	public void SendMessagesPremades() => AddRequest(RequestType.MESSAGE_PREMADE, "Fill blank to avoid the empty verification");

	// Generic add request
	private void AddRequest<T>(RequestType requestType, T requestDatas)
	{
		if (!_client)
			return;

		Logger.Write($"[{_client.OwnerClientId}] Add Request {requestType}");
		string datas = JsonUtility.ToJson(requestDatas);

		_client.AddRequest(new Request(requestType, datas));
	}
	#endregion
}