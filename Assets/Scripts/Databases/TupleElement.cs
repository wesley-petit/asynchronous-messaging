using UnityEngine;
using System;

// Each name (attribute and type) must be the same as the JSON
/* ==================================================== Tuple Element ======================================================== */
// Interface with a tuple element
public interface ITuple
{
	int Id();

	void ShowContent();

	bool IsNull();
}

[Serializable]
public struct Messages : ITuple
{
	[SerializeField] private int Message_Id;
	[SerializeField] private string Message_Content;
	[SerializeField] private string Message_Time;
	[SerializeField] private int User_Id;

	#region Getter
	public string MessageContent => Message_Content;
	public string MessageTime => Message_Time;
	public int UserId => User_Id;
	#endregion

	public Messages(int messageId, string messageContent, string messageTime, int userId)
	{
		Message_Id = messageId;
		Message_Content = messageContent;
		Message_Time = messageTime;
		User_Id = userId;
	}
	
	public int Id() => Message_Id;

	public bool IsNull() => Message_Content == null || Message_Time == null || Id() < 0;

	public void ShowContent()
	{
		Debug.Log($"Message Id : {Message_Id}");
		Debug.Log($"Message Content : {Message_Content}");
		Debug.Log($"Message_Time : {Message_Time}");
		Debug.Log($"User Id : {User_Id}");
	}
}

[Serializable]
public struct Positions_Messages : ITuple
{
	[SerializeField] private int Position_Id;
	// Double[] because we can't save a vector 3 in a json file
	[SerializeField] private double[] Position_Message;
	[SerializeField] private double[] Rotation_Message;
	[SerializeField] private int Message_Id;

	#region Getter
	public Vector3 PositionMessage => CastToVector3(Position_Message);
	public Vector3 RotationMessage => CastToVector3(Rotation_Message);
	public int MessageId => Message_Id;
	#endregion

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

	#region Cast DoubleArray and Vector3
	public double[] CastToDoubleArray(Vector3 vector)
	{
		double[] vectorDouble = { vector.x, vector.y, vector.z };
		return vectorDouble;
	}
	public Vector3 CastToVector3(double[] vectorDouble) => new Vector3((float)vectorDouble[0], (float)vectorDouble[1], (float)vectorDouble[2]);
	#endregion

	public int Id() => Position_Id;

	public bool IsNull() => Position_Message == null || Rotation_Message == null || Id() < 0;

	public void ShowContent()
	{
		Debug.Log($"Position Id : {Position_Id}");
		Debug.Log($"Position Message : {Position_Message}");
		Debug.Log($"Rotation Message : {Rotation_Message}");
		Debug.Log($"Message Id : {Message_Id}");
	}
}

[Serializable]
public struct Users : ITuple
{
	[SerializeField] private int User_Id;
	[SerializeField] private string User_Name;

	public string UserName => User_Name;

	public Users(int userId, string userName)
	{
		User_Id = userId;
		User_Name = userName;
	}

	public int Id() => User_Id;

	public bool IsNull() => User_Name == null || Id() < 0;

	public void ShowContent()
	{
		Debug.Log($"User Id : {User_Id}");
		Debug.Log($"User Name : {User_Name}");
	}
}