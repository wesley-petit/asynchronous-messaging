using UnityEngine;
using System.Collections.Generic;

// Each name (attribute and type) must be the same as the JSON
// Each Save and Load value must be marked as a SerializeField
[System.Serializable]
public class Database
{
	[SerializeField] private List<Message> messages = new List<Message>();
	[SerializeField] private string[] messages_premades = new string[0];

	public List<Message> Messages => messages;
	public string[] MessagesPremades => messages_premades;

	#region Constructor
	public Database()
	{
		messages = new List<Message>();
		messages_premades = new string[0];
	}

	public Database(List<Message> messages, string[] messages_premades)
	{
		this.messages = messages;
		this.messages_premades = messages_premades;
	}
	#endregion
}
