using System;
using System.IO;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar.Collections;
using MLAPI.Serialization;
using MLAPI.Serialization.Pooled;

// Store only client requests and his getters / setters
public class ClientRequests : NetworkedBehaviour
{
	#region Public Fields
	public bool IsOwnedByTheClient => IsOwner;
	public ulong ClientId => OwnerClientId;
	public NetworkedList<ClientRequest> RequestOut { get; private set; } // Client to server
		= new NetworkedList<ClientRequest>(NetworkedSettings.ServerRead, new NetworkedList<ClientRequest>());
	public NetworkedList<ClientRequest> RequestIn { get; private set; }  // Server to client
		= new NetworkedList<ClientRequest>(NetworkedSettings.OwnerRead, new NetworkedList<ClientRequest>());
	#endregion

	public Action OnUpdateData = null;

	private void Start()
	{
		// Tells the MLAPI how to serialize and deserialize ClientRequest in the future.
		SerializationManager.RegisterSerializationHandlers<ClientRequest>((Stream stream, ClientRequest instance) =>
		{
			// This delegate gets ran when the MLAPI want's to serialize a Url type to the stream.
			using (PooledBitWriter writer = PooledBitWriter.Get(stream))
			{
				//writer.WriteStringPacked(instance);
			}
		}, (Stream stream) =>
		{
			// This delegate gets ran when the MLAPI want's to deserialize a Url type from the stream.
			using (PooledBitReader reader = PooledBitReader.Get(stream))
			{
				//return new ClientRequest(reader.ReadStringPacked().ToString());
				return new ClientRequest();
			}
		});

		//MemoryStream memoryStream = new MemoryStream();

		//using (PooledBitWriter writer = PooledBitWriter.Get(memoryStream))
		//{
		//	writer.WriteObjectPacked(clientRequest);
		//	// Write here
		//}
		//memoryStream.Close();

		//using (PooledBitReader reader = PooledBitReader.Get(myStreamToReadFrom))
		//{
		//	// Read here
		//}
	}

	#region Request And Response
	// Add and verify a request
	public void AddRequest(ClientRequest clientRequest)
	{
		if (clientRequest.IsEmpty)
		{
			Logger.Write($"[{ClientId}] Empty request {clientRequest.RequestType}", LogType.WARNING);
			return;
		}

		//RequestOut.Add(new ClientRequest(clientRequest, this));
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
