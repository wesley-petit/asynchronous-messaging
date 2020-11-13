using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// Give Access in data
public class DatabaseController : MonoBehaviour
{ 
	[SerializeField] private Database _database = new Database();
	[SerializeField] private float _aroundThreshold = 5f;			// Define when a message is near a player

	// Wraper
	public string[] GetMessagesPremades => _database.MessagesPremades;
	
	private MessageComparer _comparer = new MessageComparer();      // To verify doublons

	private void Awake()
	{
		LoadDatabase();
		ClearDoublons();
	}

	#region ServerInteract
	public Message[] GetMessagesByPlayerPosition(Vector3 playerPosition)
	{
		/// HashSet - Doesn't allow doublons and it's more efficient to add an item (like a List)
		/// Linq - Take all position in a certain area around the player
		/// ToArray - HashSet can't be serialize in editor or in a file
		return new HashSet<Message>(_database.Messages
								.Where(k => Vector3.Distance(k.GetPositionMessage, playerPosition) < _aroundThreshold),
								_comparer)
								.ToArray();
	}

	public void Insert(Message message)
	{
		Logger.Write($"Add a new Message...");

		// We verify his value
		if (message == null)
		{
			Logger.Write($"Can't add a null Message", LogType.ERROR);
			return;
		}

		if (message.ContainsNullValues || message.IsEmpty)
		{
			Logger.Write($"Can't add a Message which contains null values", LogType.ERROR);
			return;
		}

		if (IsItInDatabase(message))
		{
			Logger.Write($"Can't add a Message as a doublon", LogType.ERROR);
			return;
		}

		_database.Messages.Add(message);
		Logger.Write($"Add a new Message created by {message.GetUserName} in {message.GetPositionMessage}");
	}
	#endregion

	#region SaveAndLoad
	[ContextMenu("Save Database")]
	public void SaveDatabase()
	{
		Logger.Write("Save Database...");
		string jsonData = JsonUtility.ToJson(_database, true);
		FileManagement.Write(FileNameConst.DATABASE, jsonData);
	}

	// Load Database
	[ContextMenu("Load Database")]
	public void LoadDatabase()
	{
		Logger.Write("Load Database...");

		string jsonData = FileManagement.Read(FileNameConst.DATABASE);
		if (jsonData == "")
		{
			Logger.Write("Failed to Load Database...", LogType.ERROR);
			return;
		}
		_database = JsonUtility.FromJson<Database>(jsonData);
	}

	[ContextMenu("Clear Doublons")]
	public void ClearDoublons()
	{
		Logger.Write("Clear Doublons");

		//HashSet delete doublons, produce less garbage collector than Linq
		List<Message> messages = new HashSet<Message>(_database.Messages, _comparer).ToList();
		string[] messagesPremades = new HashSet<string>(_database.MessagesPremades).ToArray();

		_database = new Database(messages, messagesPremades);
	}
	#endregion

	// To verify if a Message will be add as a doublon
	private bool IsItInDatabase(Message message) => _database.Messages.Contains(message, _comparer);
}