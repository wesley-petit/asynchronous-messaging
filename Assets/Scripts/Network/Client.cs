using UnityEngine;
using MLAPI.NetworkedVar;
using MLAPI;

public class Client : NetworkedBehaviour
{
	public NetworkedVar<string> HelloWorld =						// Request
		new NetworkedVar<string>(NetworkedSettings.Everyone, "");
	public NetworkedVar<string> Result =							// Result
		new NetworkedVar<string>(NetworkedSettings.Everyone, "");

	// Start means the first function / first connection
	private void Start()
	{
		if (IsLocalPlayer)
		{
			Debug.Log("---------------------------------------- Local ----------------------------------------");
		}
		else
		{
			Debug.Log("---------------------------------------- Not Local ----------------------------------------");
		}
	}

	// Update Client Logic (read request or read result)
	private void Update()
	{
		// It will be check in the Start, so we can verify only once
		if (IsLocalPlayer)
		{
			UpdateInLocal();
		}
		else
		{
			UpdateInServer();
		}
	}

	// For this object and in this project, Destroy means a deconnection
	private void OnDestroy()
	{
		if (IsLocalPlayer)
		{
			Debug.Log("---------------------------------------- Good Bye - Local ----------------------------------------");
		}
		else
		{
			Debug.Log("---------------------------------------- Good Bye - Not Local ----------------------------------------");
		}
	}

	private void UpdateInLocal()
	{
		if (Result.Value != "")
		{
			Debug.LogWarning(Result.Value);
			Result.Value = "";
		}
	}

	private void UpdateInServer()
	{
		if (HelloWorld.Value != "")
		{
			Result.Value = ServerManager.Instance.RequestHelloWorld(HelloWorld.Value);
			HelloWorld.Value = "";
		}
	}

	// Use in editor to test / ping server with a Good Bye response
	[ContextMenu("AddHelloWorld")]
	public void AddHelloWorld() => HelloWorld.Value += "Hello, World ! ";
}
