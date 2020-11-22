using System;
using UnityEngine;

// Add request in client requests with the correct datas
// Take the server response and give it to other instance
public class ClientOutHandler : MonoBehaviour
{
	[SerializeField] private ClientRequests _clientRequests = null;
	[SerializeField] private float _timerMax = 10f;                 // Time limits where we send request
	[SerializeField] private Vector3 _playerPosition = Vector3.zero;

	private float _timer = 0f;

	#region Unity Methods
	private void Start()
	{
		if (!_clientRequests)
		{
			Logger.Write("Client Requests is undefined", LogType.ERROR);
			return;
		}

		// TODO Remove it
		AddTypeRequest(RequestType.SCAN_MESSAGES);
	}

	private void Update()
	{
		_timer += Time.deltaTime;

		if (_timerMax < _timer)
		{
			_timer = 0f;

			if (!_clientRequests)
				return;

			foreach (var response in _clientRequests.RequestIn)
			{
				ReadResponse(response);
			}

			UpdateDatas();
		}
	}
	#endregion

	#region Request And Response
	// With a specefic type, it will add the correct value in the client request
	public void AddTypeRequest(RequestType requestType)
	{
		if (!_clientRequests)
			return;

		Logger.Write($"[{_clientRequests.ClientId}] Add Type Request {requestType}");
		string datas = "";

		switch (requestType)
		{
			case RequestType.SCAN_MESSAGES:
				datas = JsonUtility.ToJson(_playerPosition);
				break;

			default:
				Logger.Write($"[{_clientRequests.ClientId}] Unknow request {requestType}", LogType.WARNING);
				break;
		}
		
		_clientRequests.AddRequest(new ClientRequest(requestType, datas));
	}

	private void ReadResponse(ClientRequest response)
	{
		// TODO Remove it
		response.ShowContent();
		Logger.Write($"[{_clientRequests.ClientId}] Response from Server of {response.RequestType}");

		switch (response.RequestType)
		{
			case RequestType.SCAN_MESSAGES:
				try
				{
					// Input
					Messages messages = JsonUtility.FromJson<Messages>(response.Datas);

					// Give last value
					// TODO Remove it
					//foreach (var message in messages.MessageArray)
					//{
					//	message.ShowContent();
					//}
				}
				catch (Exception e)
				{
					Logger.Write(e.ToString(), LogType.ERROR);
					return;
				}
				break;

			case RequestType.ADD_MESSAGE:
				break;

			default:
				Logger.Write($"Unknow response {response.RequestType}", LogType.WARNING);
				break;
		}
	}
	#endregion
	
	private void UpdateDatas()
	{
		_clientRequests.UpdateDatas();
		//_clientDatas.InvokeServerRpc(_clientDatas.UpdateData);
	}
}