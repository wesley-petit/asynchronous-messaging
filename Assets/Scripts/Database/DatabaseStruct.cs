using UnityEngine;
using System.Collections.Generic;

// Each name (variable and type) must be the same as the JSON
[System.Serializable]
public struct Database
{
	[SerializeField] private List<Messages> messages;
	[SerializeField] private List<Positions_Messages> positions_messages;
	[SerializeField] private List<Users> users;
	[SerializeField] private string[] messages_premades;

	public Database(List<Messages> messages, List<Positions_Messages> positions_messages, List<Users> users, string[] messages_premades)
	{
		this.messages = messages;
		this.positions_messages = positions_messages;
		this.users = users;
		this.messages_premades = messages_premades;
	}
	public void Initialize()
	{
		messages = new List<Messages>();
		positions_messages = new List<Positions_Messages>();
		users = new List<Users>();
		messages_premades = new string[0];
	}

	public void InsertMessage(Messages message) => messages.Add(message);
	public void InsertPositionMessage(Positions_Messages positionMessage) => positions_messages.Add(positionMessage);
	public void InsertUser(Users user) => users.Add(user);
}

/* ==================================================== Table ======================================================== */
[System.Serializable]
public struct Messages
{
	public int Message_Id;
	public string Message_Content;
	public string Message_Time;
	public int User_Id;

	public Messages(int messageId, string messageContent, string messageTime, int userId)
	{
		Message_Id = messageId;
		Message_Content = messageContent;
		Message_Time = messageTime;
		User_Id = userId;
	}
}

[System.Serializable]
public struct Positions_Messages
{
	public int Position_Id;
	// Double[] because we can't save vector 3 in a json file
	public double[] Position_Message;
	public double[] Rotation_Message;
	public int Message_Id;

	public Positions_Messages(int positionId, double[] positionMessage, double[] rotationMessage, int messageId)
	{
		Position_Id = positionId;
		Position_Message = positionMessage;
		Rotation_Message = rotationMessage;
		Message_Id = messageId;
	}

	public Positions_Messages(int positionId, Vector3 positionMessage, Vector3 rotationMessage, int messageId)
	{
		Position_Id = positionId;
		Position_Message = new double[3];
		Rotation_Message = new double[3];
		Message_Id = messageId;

		// Give value after initializing
		Position_Message = CastToDoubleArray(positionMessage);
		Rotation_Message = CastToDoubleArray(rotationMessage);
	}

	public double[] CastToDoubleArray(Vector3 vector)
	{
		double[] vectorDouble = { vector.x, vector.y, vector.z };
		return vectorDouble;
	}
}

[System.Serializable]
public struct Users
{
	public int User_Id;
	public string User_Name;

	public Users(int userId, string userName)
	{
		User_Id = userId;
		User_Name = userName;
	}
}