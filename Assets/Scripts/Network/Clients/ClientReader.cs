using UnityEngine;

// Read server response and give it to other instance
public class ClientReader : MonoBehaviour
{
	[SerializeField] private Client _client = null;

	private UIManager UI => UIManager.Instance;

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

	private void ReadResponse(Request response)
	{
		RequestType requestType = response.RequestType;
		string datas = response.Datas;

		Logger.Write($"[{_client.OwnerClientId}] Response from Server of {requestType}");

		// It can be reduce with an interface
		switch (requestType)
		{
			case RequestType.SCAN_MESSAGES:
				// Input
				Messages messages = JsonUtility.FromJson<Messages>(datas);

				// Output
				UI.SetMessageArrival(messages.GetMessages);
				break;

			case RequestType.MESSAGE_PREMADE:
				// Input
				MessagesPremades messagesPremades = JsonUtility.FromJson<MessagesPremades>(datas);

				// Output
				UI.SetMessagePremade(messagesPremades.GetMessagesPremades);
				break;

			case RequestType.PING:
				// Input
				Ping ping = JsonUtility.FromJson<Ping>(datas);
				ping.CalcultatePing();

				// Output
				UI.SetPing(ping.GetPing);
				break;

			default:
				Logger.Write($"Unknow response {requestType}", LogType.WARNING);
				break;
		}
	}
}