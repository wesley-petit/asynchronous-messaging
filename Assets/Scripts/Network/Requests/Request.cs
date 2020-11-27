using MLAPI.Serialization;
using MLAPI.Serialization.Pooled;
using System.IO;

// Struct use in the queue request
public class Request : IBitWritable
{
	public RequestType RequestType { get; private set; }
	public string Datas { get; private set; }
	public Client Client { get; private set; } // Store the result of a request

	public bool IsNull => !Client;
	public bool IsEmpty => Datas == "";

	#region Constructors
	// Client datas is null to have the same request in client and server
	public Request(RequestType requestType, string datas)
	{
		RequestType = requestType;
		Datas = datas;
		Client = null;
	}

	// Client handler will add his datas before sending
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
	public void Read(Stream stream)
	{
		using (PooledBitReader reader = PooledBitReader.Get(stream))
		{
			Datas = reader.ReadStringPacked().ToString();
			RequestType = (RequestType)reader.ReadByte();
			Client = null;
		}
	}

	public void Write(Stream stream)
	{
		using (PooledBitWriter writer = PooledBitWriter.Get(stream))
		{
			writer.WriteStringPacked(Datas);
			writer.WriteByte((byte)RequestType);
		}
	}

	public void ShowContent()
	{
		UnityEngine.Debug.Log("--------------------------------------");
		UnityEngine.Debug.Log($"Request Type : {RequestType}");
		UnityEngine.Debug.Log($"Datas : {Datas}");
		UnityEngine.Debug.Log($"Client : {Client}");
		UnityEngine.Debug.Log("--------------------------------------");
	}
}