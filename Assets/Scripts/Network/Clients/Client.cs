using UnityEngine;
using MLAPI.NetworkedVar;
using MLAPI;
using MLAPI.Messaging;

public class Client : NetworkedBehaviour
{
	public override void NetworkStart()
	{
		if (IsClient)
		{
			int rnd = Random.Range(0, int.MaxValue);

			Debug.LogFormat("Pining server with number {0}", rnd);

			InvokeServerRpc("HelloWorld");
		}
	}

	[ClientRPC]
	public void PingClient(int number)
	{
		Debug.LogFormat("Server replied with {0}!", number);
	}

	[ServerRPC]
	public void HelloWorld()
	{
		//Result.Value = ServerManager.Instance.RequestHelloWorld("Hello, World ! ");
		Debug.LogError(Result.Value);
	}

	//public NetworkedVar<string> HelloWorld =                        // Request
	//	new NetworkedVar<string>(NetworkedSettings.Everyone, "");
	public NetworkedVar<string> Result =                            // Result
		new NetworkedVar<string>(NetworkedSettings.Everyone, "");

	//[SerializeField] private ulong _clientId = 10;

	//// Start means the first function / first connection
	//private void Start()
	//{
	//	if (IsOwner)
	//	{
	//		Debug.Log("---------------------------------------- Local ----------------------------------------");
	//	}
	//	else
	//	{
	//		Debug.Log("---------------------------------------- Not Local ----------------------------------------");
	//	}
	//}

	//// Update Client Logic (read request or read result)
	//private void Update()
	//{
	//	// It will be check in the Start, so we can verify only once
	//	if (IsOwner)
	//	{
	//		UpdateInClient();
	//	}
	//	else
	//	{
	//		UpdateInServer();
	//	}
	//}

	//// For this object and in this project, Destroy means a deconnection
	//private void OnDestroy()
	//{
	//	if (IsOwner)
	//	{
	//		Debug.Log("---------------------------------------- Good Bye - Local ----------------------------------------");
	//	}
	//	else
	//	{
	//		Debug.Log("---------------------------------------- Good Bye - Not Local ----------------------------------------");
	//	}
	//}

	//private void UpdateInClient()
	//{
	//	if (Result.Value != "")
	//	{

	//		Debug.LogWarning(Result.Value);
	//		Result.Value = "";
	//	}
	//}

	//private void UpdateInServer()
	//{
	//	if (HelloWorld.Value != "")
	//	{
	//		MemoryStream memStream = new MemoryStream();
	//		StreamWriter writer = new StreamWriter(memStream);
	//		writer.Write("test display");

	//		//Sending
	//		CustomMessagingManager.SendNamedMessage("myMessageName", NetworkingManager.Singleton.LocalClientId, memStream); //Channel is optional.
	//		writer.Close();
	//		Result.Value = ServerManager.Instance.RequestHelloWorld(HelloWorld.Value);
	//		HelloWorld.Value = "";
	//	}
	//}

	//// Use in editor to test / ping server with a Good Bye response
	//[ContextMenu("AddHelloWorld")]
	//public void AddHelloWorld() => HelloWorld.Value += "Hello, World ! ";
}
