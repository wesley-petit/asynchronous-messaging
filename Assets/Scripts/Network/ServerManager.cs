using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Serialization.Pooled;
using System.Text;

public class ServerManager : MonoBehaviour
{
	public static ServerManager Instance { get; private set; } = null;

	#region Singleton And Register Callbacks
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogError($"There is two Singleton of the same type {typeof(ServerManager)}");
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		NetworkingManager.Singleton.StartServer();

		if (!NetworkingManager.Singleton.IsServer)
		{
			return;
		}

		Debug.Log("Start Server");

		NetworkingManager.Singleton.OnClientConnectedCallback += OnClientConnected;
		NetworkingManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

		//Receiving
		CustomMessagingManager.RegisterNamedMessageHandler("myMessageName", (senderClientId, stream) =>
		{
			using (PooledBitReader reader = PooledBitReader.Get(stream))
			{
				StringBuilder stringBuilder = reader.ReadString(); //Example
				string message = stringBuilder.ToString();
				Debug.LogError($"Message : {message}");
			}
		});
	}
	#endregion

	#region Callbacks
	private void OnClientConnected(ulong clientId)
	{
		Debug.Log($"[{clientId}] New Client");

		// Spawn item 
		foreach (var item in NetworkingManager.Singleton.NetworkConfig.NetworkedPrefabs)
		{
			if (item.Prefab && !item.PlayerPrefab)
			{
				if (item.Prefab.GetComponent<NetworkedObject>())
				{
					NetworkedObject networkedObject = Instantiate(item.Prefab).GetComponent<NetworkedObject>();
					networkedObject.SpawnWithOwnership(clientId);

					// Hide object in alla clients, except the owner
					foreach (var client in NetworkingManager.Singleton.ConnectedClientsList)
					{
						if (client.ClientId == networkedObject.OwnerClientId) { continue; }

						networkedObject.NetworkHide(client.ClientId);
					}

					// Callback use by other client, to know if the object will be visible or not
					networkedObject.CheckObjectVisibility = (id) => false;
				}
			}
		}
	}

	private void OnClientDisconnected(ulong clientId)
	{
		Debug.Log($"[{clientId}] Client disconnected --- Good Bye!");
	}
	#endregion

	// If it's receive Hello, World !, he sends Good Bye
	public string RequestHelloWorld(string content)
	{
		if (content == "Hello, World ! ")
		{
			return "Good Bye";
		}

		return "No Connection";
	}
}