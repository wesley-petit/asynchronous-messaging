using UnityEngine;
using System.Collections.Generic;
using System;

// Each name (attribute and type) must be the same as the JSON
[Serializable]
public class Database
{
	[SerializeField] private List<Messages> messages = new List<Messages>();
	[SerializeField] private List<Positions_Messages> positions_messages = new List<Positions_Messages>();
	[SerializeField] private List<Users> users = new List<Users>();
	[SerializeField] private string[] messages_premades = new string[0];

	#region Getter
	public List<Messages> Messages => messages;
	public List<Positions_Messages> Positions_Messages => positions_messages;
	public List<Users> Users => users;
	public string[] Messages_Premades => messages_premades;
	#endregion

	public Database()
	{
		messages = new List<Messages>();
		positions_messages = new List<Positions_Messages>();
		users = new List<Users>();
		messages_premades = new string[0];
	}

	public Database(List<Messages> messages, List<Positions_Messages> positions_messages, List<Users> users, string[] messages_premades)
	{
		this.messages = messages;
		this.positions_messages = positions_messages;
		this.users = users;
		this.messages_premades = messages_premades;
	}

	public void AddMessage(Messages message) => messages.Add(message);
	public void AddPositionMessage(Positions_Messages positionMessage) => positions_messages.Add(positionMessage);
	public void AddUser(Users user) => users.Add(user);
}
