using UnityEngine;
using System.Collections.Generic;

// Each name (attribute and type) must be the same as the JSON
[System.Serializable]
public class Database
{
	[SerializeField] private List<Message> messages = new List<Message>();
	[SerializeField] private List<Position_Messages> positions_messages = new List<Position_Messages>();
	[SerializeField] private List<User> users = new List<User>();
	[SerializeField] private string[] messages_premades = new string[0];

	#region Getter
	public List<Message> Messages => messages;
	public List<Position_Messages> Positions_Messages => positions_messages;
	public List<User> Users => users;
	public string[] Messages_Premades => messages_premades;
	#endregion

	public Database()
	{
		messages = new List<Message>();
		positions_messages = new List<Position_Messages>();
		users = new List<User>();
		messages_premades = new string[0];
	}

	public Database(List<Message> messages, List<Position_Messages> positions_messages, List<User> users, string[] messages_premades)
	{
		this.messages = messages;
		this.positions_messages = positions_messages;
		this.users = users;
		this.messages_premades = messages_premades;
	}
}
