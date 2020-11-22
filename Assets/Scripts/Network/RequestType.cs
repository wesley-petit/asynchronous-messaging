using UnityEngine;

// TODO Remove it
[System.Serializable]
public enum RequestType
{
	SCAN_MESSAGES,
	ADD_MESSAGE
}

// Struct use in the queue request
// TODO Remove it
[System.Serializable]
public struct ClientRequest
{
	[SerializeField] public RequestType RequestType { get; private set; }
	[SerializeField] public string Datas { get; private set; }
	[SerializeField] public ClientRequests ClientDatas { get; private set; }            // Store the result of a request

	public bool IsNull => !ClientDatas;
	public bool IsEmpty => Datas == "";

	// Client datas is null to have the same request in client and server
	public ClientRequest(RequestType requestType, string datas)
	{
		RequestType = requestType;
		Datas = datas;
		ClientDatas = null;
	}

	// Client handler will add his datas before sending
	public ClientRequest(ClientRequest clientRequest, ClientRequests clientDatas)
	{
		RequestType = clientRequest.RequestType;
		Datas = clientRequest.Datas;
		ClientDatas = clientDatas;
	}

	public ClientRequest(RequestType requestType, string datas, ClientRequests clientDatas)
	{
		RequestType = requestType;
		Datas = datas;
		ClientDatas = clientDatas;
	}

	public void ShowContent()
	{
		Debug.Log("--------------------------------------");
		Debug.Log($"Request Type : {RequestType}");
		Debug.Log($"Datas : {Datas}");
		Debug.Log($"Client Datas : {ClientDatas}");
		Debug.Log("--------------------------------------");
	}
}