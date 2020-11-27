using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar.Collections;

// Store only client requests and his getters / setters
public class Client : NetworkedBehaviour
{
	#region Public Fields
	public NetworkedList<Request> Request { get; private set; } // Client to Server
		= new NetworkedList<Request>(NetworkedSettings.OwnerOnly, new NetworkedList<Request>());
	public List<Request> Response { get; private set; }         // Server to Client
		= new List<Request>();
	#endregion

	#region Request And Response
	// Add and verify a request
	public void AddRequest(Request clientRequest)
	{
		if (clientRequest.IsEmpty)
			return;

		Request.Add(clientRequest);
	}

	[ClientRPC]
	// Add and verify a response
	public void AddResponse(Request serverResponse)
	{
		if (serverResponse.IsEmpty)
			return;

		Response.Add(serverResponse);
	}
	#endregion
}
