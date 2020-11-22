using System;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar.Collections;

// Store only client requests and his getters / setters
public class ClientRequests : NetworkedBehaviour
{
	#region Public Fields
	public bool IsOwnedByTheClient => IsOwner;
	public ulong ClientId => OwnerClientId;
	public NetworkedList<ClientRequest> RequestOut { get; private set; } // Client to server
		= new NetworkedList<ClientRequest>(NetworkedSettings.Everyone, new NetworkedList<ClientRequest>());
	public NetworkedList<ClientRequest> RequestIn { get; private set; }  // Server to client
		= new NetworkedList<ClientRequest>(NetworkedSettings.Everyone, new NetworkedList<ClientRequest>());
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

		RequestOut.Add(new ClientRequest(clientRequest, this));
	}

	// Add and verify a response
	public void AddResponse(ClientRequest serverResponse)
	{
		if (serverResponse.IsEmpty)
		{
			Logger.Write($"Empty request {serverResponse.RequestType} from Server", LogType.WARNING);
			return;
		}

		RequestIn.Add(serverResponse);
	}
	#endregion

	[ServerRPC(RequireOwnership = true)]
	public void UpdateDatas() => OnUpdateData?.Invoke();
}
