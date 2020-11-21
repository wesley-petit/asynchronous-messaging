using UnityEngine;
using MLAPI;

public class ServerManager : MonoBehaviour
{
	public static ServerManager Instance { get; private set; } = null;

	[SerializeField] private DatabaseController _dbController = new DatabaseController();

	#region Singleton And Register Callbacks
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Logger.Write($"There is two Singleton of the same type {typeof(ServerManager)}");
			Destroy(gameObject);
		}

		LoadDatabase();
		ClearDoublons();
	}

	private void Start()
	{
		Logger.Write("Start Server...");
		NetworkingManager.Singleton.StartServer();

		if (!NetworkingManager.Singleton.IsServer)
		{
			Logger.Write("Server doesn't start", LogType.ERROR);
			return;
		}

		NetworkingManager.Singleton.OnClientConnectedCallback += OnClientConnected;
		NetworkingManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
	}
	#endregion

	#region Callbacks
	private void OnClientConnected(ulong clientId)
	{
		Logger.Write($"[{clientId}] New Client");

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
		Logger.Write($"[{clientId}] Client disconnected --- Good Bye!");
	}
	#endregion

	#region DatabaseContextMenu
	[ContextMenu("Save Database")]
	private void SaveDatabase() => _dbController.SaveDatabase();

	[ContextMenu("Load Database")]
	private void LoadDatabase() => _dbController.LoadDatabase();

	[ContextMenu("Clear Doublons")]
	private void ClearDoublons() => _dbController.ClearDoublons();
	#endregion

	// TODO Remove it
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