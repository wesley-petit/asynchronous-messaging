using MLAPI.Serialization.Pooled;

// Struct use in the queue request
// Interface to transfer data with MLAPI
public class Request : MLAPI.Serialization.IBitWritable
{
	public RequestType RequestType { get; private set; }
	public string Datas { get; private set; }
	public Client Client { get; private set; }          // Reference of a sender

	public bool IsNull => !Client;
	public bool IsEmpty => string.IsNullOrEmpty(Datas);

	#region Constructors
	// Client is null on a client side
	public Request(RequestType requestType, string datas)
	{
		RequestType = requestType;
		Datas = datas;
		Client = null;
	}

	// Client handler will add a client reference before sending
	public Request(Request request, Client client)
	{
		RequestType = request.RequestType;
		Datas = request.Datas;
		Client = client;
	}

	public Request(RequestType requestType, string datas, Client client)
	{
		RequestType = requestType;
		Datas = datas;
		Client = client;
	}

	public Request()
	{
		RequestType = RequestType.SCAN_MESSAGES;
		Datas = "";
		Client = null;
	}
	#endregion

	// Use by MLAPI to transfer a request
	#region Read And Write
	public void Read(System.IO.Stream stream)
	{
		using (PooledBitReader reader = PooledBitReader.Get(stream))
		{
			Datas = reader.ReadStringPacked().ToString();
			RequestType = (RequestType)reader.ReadByte();
			Client = null;
		}
	}

	public void Write(System.IO.Stream stream)
	{
		using (PooledBitWriter writer = PooledBitWriter.Get(stream))
		{
			writer.WriteStringPacked(Datas);
			writer.WriteByte((byte)RequestType);
		}
	}
	#endregion

	public void ShowContent()
	{
		UnityEngine.Debug.Log("--------------------------------------");
		UnityEngine.Debug.Log($"Request Type : {RequestType}");
		UnityEngine.Debug.Log($"Datas : {Datas}");
		UnityEngine.Debug.Log($"Client : {Client}");
		UnityEngine.Debug.Log("--------------------------------------");
	}
}