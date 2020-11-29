using UnityEngine;

// Each name (attribute and type) must be the same as the JSON
/* ==================================================== Tuple Element ======================================================== */
[System.Serializable]
public class Message
{
	[SerializeField] private string User_Name = "";
	[SerializeField] private string Message_Content = "";
	[SerializeField] private string Message_Time = "";
	[SerializeField] private Vector3 Position_Message = Vector3.zero;

	#region Getter
	public string GetUserName => User_Name;
	public string GetMessageContent => Message_Content;
	public string GetMessageTime => Message_Time;
	public Vector3 GetPositionMessage => Position_Message;
	#endregion

	// string.IsNullOrEmpty will do the work in a real production
	public bool ContainsNullValues => User_Name == null || Message_Content == null || Message_Time == null;
	public bool IsEmpty => User_Name == "" || Message_Content == "" || Message_Time == "";

	public Message(string userName, string messageContent, string messageTime, Vector3 positionMessage)
	{
		User_Name = userName;
		Message_Content = messageContent;
		Message_Time = messageTime;
		Position_Message = positionMessage;
	}

	public void ShowContent()
	{
		Debug.Log("--------------------------------------");
		Debug.Log($"User Name : {User_Name}");
		Debug.Log($"Message Content : {Message_Content}");
		Debug.Log($"Message Time : {Message_Time}");
		Debug.Log($"Position Message : {Position_Message}");
		Debug.Log("--------------------------------------");
	}
}