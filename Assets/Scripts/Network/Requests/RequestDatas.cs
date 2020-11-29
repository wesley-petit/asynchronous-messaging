using System;

// Datas send in a request / Paste in a JSON
// Each Save and Load value must be marked as a SerializeField
/* ==================================================== Request Datas ======================================================== */

// Class contains only arrays to serialize an specefic array and not the original array
#region Array Class
[Serializable]
public class Messages
{
	[UnityEngine.SerializeField] private Message[] messages = new Message[0];

	public Message[] GetMessages => messages;

	public Messages()
	{
		messages = new Message[0];
	}

	public Messages(Message[] messages)
	{
		this.messages = messages;
	}
}

[Serializable]
public class MessagesPremades
{
	[UnityEngine.SerializeField] private string[] messagesPremades = new string[0];

	public string[] GetMessagesPremades => messagesPremades;

	public MessagesPremades()
	{
		messagesPremades = new string[0];
	}

	public MessagesPremades(string[] messagesPremades)
	{
		this.messagesPremades = messagesPremades;
	}
}
#endregion

[Serializable]
public class Ping
{
	[UnityEngine.SerializeField] private string _sendTime = "";
	[UnityEngine.SerializeField] private string _ping = "";

	public string GetPing => _ping;

	private DateTime GetTime => DateTime.Now;

	public Ping() => _sendTime = GetTime.ToString();

	public void CalcultatePing()
	{
		if (DateTime.TryParse(_sendTime, out DateTime sendTime))
		{
			TimeSpan diff = GetTime - sendTime;
			_ping = diff.TotalMilliseconds.ToString();
		}
	}
}