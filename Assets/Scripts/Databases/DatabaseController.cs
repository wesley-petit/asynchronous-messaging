using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// Give Access in data
public class DatabaseController : MonoBehaviour
{
	[SerializeField] private Database _mainDatabase = new Database();

	// Wraper
	public string[] Messages_Premades => _mainDatabase.Messages_Premades;

	// To generate a new Id with the max id value
	private int _maxMessageId = 0;
	private int _maxPositionMessageId = 0;
	private int _maxUserId = 0;
	private float _aroundThreshold = 5f;							// Define when a message is near a player

	private void Awake()
	{
		LoadDatabase();
		ClearDoublons();
	}

	#region Get Element By Predicate
	public HashSet<Message> GetMessagesByPlayerPosition(Vector3 playerPosition)
	{
		// Doesn't allow doublons and it's more efficient to add an item (like a List)
		HashSet<Message> messages = new HashSet<Message>();
		Message currentMessage = new Message();
		
		// Take all position in a certain area around the player
		Position_Messages[] positions = _mainDatabase.Positions_Messages.Where(
												k => Vector3.Distance(k.PositionMessage, playerPosition) < _aroundThreshold)
												.ToArray();

		// Take all messages in this positions
		foreach (var position in positions)
		{
			currentMessage = GetMessageByPredicate(k => k.Id() == position.MessageId);
			
			if (!currentMessage.IsNull())
			{
				messages.Add(currentMessage);
			}
		}

		return messages;
	}

	// Easier to use
	public Message GetMessageByPredicate(Func<Message, bool> predicate) => GetElementByPredicate(_mainDatabase.Messages, predicate);
	public Position_Messages GetPositionByPredicate(Func<Position_Messages, bool> predicate) => GetElementByPredicate(_mainDatabase.Positions_Messages, predicate);
	public User GetUserByPredicate(Func<User, bool> predicate) => GetElementByPredicate(_mainDatabase.Users, predicate);
	private T GetElementByPredicate<T>(List<T> searchList, Func<T, bool> predicate) => searchList.Where(predicate).FirstOrDefault();
	#endregion

	#region Insert Element
	public void InsertMessage(string messageContent, string messageTime, int userId)
	{
		int messageId = GenerateId(++_maxMessageId);

		_mainDatabase.Messages.Add(new Message(messageId, messageContent, messageTime, userId));
		Logger.Write($"Add a new Message with id {messageId}");
	}

	public void InsertPositionMessage(Vector3 positionMessage, Vector3 rotationMessage, int messageId)
	{
		int positionId = GenerateId(++_maxPositionMessageId);

		_mainDatabase.Positions_Messages.Add(new Position_Messages(positionId, positionMessage, rotationMessage, messageId));
		Logger.Write($"Add a new Position Message with id {positionId}");
	}

	public void InsertUser(string userName)
	{
		int userId = GenerateId(++_maxUserId);

		_mainDatabase.Users.Add(new User(userId, userName));
		Logger.Write($"Add a new User with id {userId}");
	}
	#endregion

	// If a foreign key exist or not
	#region Is This Element Exist
	// Easier to use
	public bool IsThisMessageInDatabase(int searchId) => IsThisInDatabase(_mainDatabase.Messages, k => k.Id() == searchId, searchId);
	public bool IsThisPositionMessageInDatabase(int searchId) => IsThisInDatabase(_mainDatabase.Positions_Messages, k => k.Id() == searchId, searchId);
	public bool IsThisUserInDatabase(int searchId) => IsThisInDatabase(_mainDatabase.Users, k => k.Id() == searchId, searchId);
	private bool IsThisInDatabase<T>(List<T> searchList, Func<T, bool> predicate, int searchId)
	{
		bool res = searchList.Any(predicate);
		if (!res)
		{
			Logger.Write($"Unknown {typeof(T)} - Id {searchId} doesn't exist", LogType.ERROR);
		}

		return res;
	}
	#endregion

	[ContextMenu("Save Database")]
	public void SaveDatabase()
	{
		Logger.Write("Save Database...");
		string jsonData = JsonUtility.ToJson(_mainDatabase, true);
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

		_mainDatabase = JsonUtility.FromJson<Database>(jsonData);
		InitializeMaxId();
	}

	[ContextMenu("Clear Doublons")]
	public void ClearDoublons()
	{
		Logger.Write("Clear Doublons");
		//HashSet delete doublons, produce less garbage collector than Linq
		List<Message> messages = new HashSet<Message>(_mainDatabase.Messages).ToList();
		List<Position_Messages> positions_messages = new HashSet<Position_Messages>(_mainDatabase.Positions_Messages, new PositionMessageComparer()).ToList();
		List<User> users = new HashSet<User>(_mainDatabase.Users).ToList();
		string[] messages_premades = new HashSet<string>(_mainDatabase.Messages_Premades).ToArray();

		_mainDatabase = new Database(messages, positions_messages, users, messages_premades);
	}

	private void InitializeMaxId()
	{
		_maxMessageId = GetMaxValue(_mainDatabase.Messages, k => k.Id());
		_maxPositionMessageId = GetMaxValue(_mainDatabase.Positions_Messages, k => k.Id());
		_maxUserId = GetMaxValue(_mainDatabase.Users, k => k.Id());

		Logger.Write("Initializing Max Id");
	}

	// Take the max value
	private int GetMaxValue<T>(List<T> list, Func<T, int> predicate) => list.Max(predicate);

	private int GenerateId(int maxId)
	{
		// Initialize Max Id
		if (maxId <= 0)
		{
			InitializeMaxId();
		}

		// Increment only on local
		return maxId;
	}
}