using UnityEngine;
using System.Collections.Generic;
using MLAPI;

public class ServerManager : MonoBehaviour
{
	[SerializeField] private DatabaseController _dbController = new DatabaseController();

	public static ServerManager Instance { get; private set; } = null;
	public int ConnectedClientsNumber => NetworkManager.ConnectedClientsList.Count;

	private NetworkingManager NetworkManager => NetworkingManager.Singleton;

	private Queue<Request> _requests = new Queue<Request>();

	#region Unity Methods
	// Singleton
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Logger.Write($"There is two Singleton of the same type : {typeof(ServerManager)}", LogType.ERROR);
			Destroy(gameObject);
		}

		LoadDatabase();
		ClearDoublons();
	}

	// Register Callback
	private void Start()
	{
		Logger.Write("Start Server...");
		NetworkManager.StartServer();

		if (!NetworkManager.IsServer)
		{
			Logger.Write("Server doesn't start", LogType.ERROR);
			return;
		}

		NetworkManager.OnClientConnectedCallback += OnClientConnected;
		NetworkManager.OnClientDisconnectCallback += OnClientDisconnected;
	}

	private void Update()
	{
		if (NetworkManager.IsServer && 0 < _requests.Count)
		{
			do
			{
				ApplyRequest(_requests.Dequeue());
			}
			while (0 < _requests.Count);
		}
	}
	#endregion

	#region Request
	// Add and verify a request
	public void AddRequest(Request clientRequest)
	{
		if (clientRequest.IsNull)
		{
			Logger.Write($"Null client with request : {clientRequest.RequestType}", LogType.WARNING);
			return;
		}

		if (clientRequest.IsEmpty)
		{
			Logger.Write($"[{clientRequest.Client.OwnerClientId}] Empty request {clientRequest.RequestType}", LogType.WARNING);
			return;
		}

		_requests.Enqueue(clientRequest);
	}

	private void ApplyRequest(Request clientRequest)
	{
		string response = "";
		RequestType requestType = clientRequest.RequestType;
		string datas = clientRequest.Datas;
		Client client = clientRequest.Client;

		switch (requestType)
		{
			case RequestType.SCAN_MESSAGES:
				// Input
				Vector3 position = JsonUtility.FromJson<Vector3>(datas);

				// Calculate
				Messages messages = _dbController.GetMessagesByPlayerPosition(position);
				response = JsonUtility.ToJson(messages);
				break;

			case RequestType.ADD_MESSAGE:
				// Input
				Message addMessage = JsonUtility.FromJson<Message>(datas);

				_dbController.Insert(addMessage);
				break;

			case RequestType.MESSAGE_PREMADE:
				MessagesPremades messagePremades = new MessagesPremades(_dbController.GetMessagesPremades);
				response = JsonUtility.ToJson(messagePremades);
				break;

			case RequestType.PING:
				// Ping calculate in the client
				Ping ping = JsonUtility.FromJson<Ping>(datas);
				ping.CalcultatePing();
				response = datas;
				break;

			default:
				Logger.Write($"Unknow request {requestType} from [{client.OwnerClientId}]", LogType.WARNING);
				break;
		}

		// Output
		client.InvokeClientRpcOnOwner(client.AddResponse, new Request(requestType, response));
	}
	#endregion

	#region Callbacks
	private void OnClientConnected(ulong clientId)
	{
		Logger.Write($"[{clientId}] New Client");

		// Spawn item 
		foreach (var item in NetworkManager.NetworkConfig.NetworkedPrefabs)
			SpawnAndHide(clientId, item);
	}

	private void OnClientDisconnected(ulong clientId)
	{
		Logger.Write($"[{clientId}] Client disconnected --- Good Bye!");
	}

	private void SpawnAndHide(ulong clientId, MLAPI.Configuration.NetworkedPrefab item)
	{
		if (item.Prefab && !item.PlayerPrefab)
		{
			if (item.Prefab.GetComponent<NetworkedObject>())
			{
				NetworkedObject networkedObject = Instantiate(item.Prefab).GetComponent<NetworkedObject>();
				networkedObject.SpawnWithOwnership(clientId);
				networkedObject.name = $"Client_{clientId}";

				HideToOtherClients(networkedObject);
			}
		}
	}

	private void HideToOtherClients(NetworkedObject networkedObject)
	{
		// Hide object in all clients, except the owner
		foreach (var client in NetworkManager.ConnectedClientsList)
		{
			if (client.ClientId == networkedObject.OwnerClientId) { continue; }

			networkedObject.NetworkHide(client.ClientId);
		}

		// Callback use by other client, to know if the object will be visible or not
		networkedObject.CheckObjectVisibility = (id) => false;
	}
	#endregion

	#region DatabaseContextMenu
	[ContextMenu("Save Database")] public void SaveDatabase() => _dbController.SaveDatabase();
	[ContextMenu("Load Database")] public void LoadDatabase() => _dbController.LoadDatabase();
	[ContextMenu("Clear Doublons")] public void ClearDoublons() => _dbController.ClearDoublons();
	#endregion
}