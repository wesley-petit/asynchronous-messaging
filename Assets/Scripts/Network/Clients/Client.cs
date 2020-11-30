using System.Collections.Generic;
using MLAPI.NetworkedVar.Collections;

// Store only client requests and response
// It offers getters and Add Request / Respone
public class Client : MLAPI.NetworkedBehaviour
{
	#region Public Fields
	public NetworkedList<Request> Request { get; private set; } // Client to Server
		= new NetworkedList<Request>(NetworkedSettings.OwnerOnly, new NetworkedList<Request>());
	public List<Request> Response { get; private set; }         // Server to Client
		= new List<Request>();
	#endregion

	#region Register Unregister
	private void Start()
	{
		// Register for easier Send Request
		if (IsOwner)
			UIManager.Instance.Register(this);
	}

	private void OnDestroy()
	{
		// Unregister in the disconnect to avoid a null reference
		if (IsOwner)
			UIManager.Instance.Unregister();
	}
	#endregion

	#region Request And Response
	// Add and verify a request
	public void AddRequest(Request clientRequest)
	{
		if (clientRequest.IsEmpty)
			return;

		Request.Add(clientRequest);
	}

	[MLAPI.Messaging.ClientRPC]
	// Add and verify a response
	public void AddResponse(Request serverResponse)
	{
		if (serverResponse.IsEmpty)
			return;

		Response.Add(serverResponse);
	}
	#endregion
}
