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

	public System.Action OnSendRequest = null;

	#region Request And Response
	// Add and verify a request
	public void AddRequest(Request clientRequest)
	{
		if (clientRequest.IsEmpty)
		{
			Logger.Write($"[{OwnerClientId}] Empty request {clientRequest.RequestType}", LogType.WARNING);
			return;
		}

		Request.Add(clientRequest);
	}

	[ClientRPC]
	// Add and verify a response
	public void AddResponse(Request serverResponse)
	{
		if (serverResponse.IsEmpty)
		{
			Logger.Write($"Empty request {serverResponse.RequestType} from Server", LogType.WARNING);
			return;
		}

		Response.Add(serverResponse);
	}
	#endregion

	[ServerRPC(RequireOwnership = true)]
	public void SendRequest() => OnSendRequest?.Invoke();
}
