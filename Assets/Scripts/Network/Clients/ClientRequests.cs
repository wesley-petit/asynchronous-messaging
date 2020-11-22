using System;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;

// Store only client requests and his getters / setters
public class ClientRequests : NetworkedBehaviour
{
	#region Public Fields
	public bool IsOwnedByTheClient => IsOwner;
	public ulong ClientId => 9000; // OwnerClientId;
	public Queue<ClientRequest> RequestOut { get; private set; }            // Client to server
		= new Queue<ClientRequest>();
	public Queue<ClientRequest> RequestIn { get; private set; }            // Server to client
		= new Queue<ClientRequest>();
	#endregion

	public Action OnUpdateData = null;

	#region Request And Response
	// Add and verify a request
	public void AddRequest(ClientRequest clientRequest)
	{
		if (clientRequest.IsEmpty)
		{
			Logger.Write($"[{ClientId}] Empty request {clientRequest.RequestType}", LogType.WARNING);
			return;
		}

		RequestOut.Enqueue(new ClientRequest(clientRequest, this));
	}

	// Add and verify a response
	public void AddResponse(ClientRequest serverResponse)
	{
		if (serverResponse.IsEmpty)
		{
			Logger.Write($"Empty request {serverResponse.RequestType} from Server", LogType.WARNING);
			return;
		}

		RequestIn.Enqueue(serverResponse);
	}
	#endregion

	[ServerRPC(RequireOwnership = true)]
	public void UpdateDatas() => OnUpdateData?.Invoke();
}
