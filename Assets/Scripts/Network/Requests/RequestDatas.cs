// Datas send in a request / Paste in a JSON
// Each Save and Load value must be marked as a SerializeField
/* ==================================================== Request Datas ======================================================== */
[System.Serializable]
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

[System.Serializable]
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

[System.Serializable]
public class Ping
{
	[UnityEngine.SerializeField] private int _sendTime;
	[UnityEngine.SerializeField] private string _ping;

	public string GetPing => _ping;

	public Ping() => _sendTime = GetTime();

	public void CalcultatePing()
	{
		int diffTime = GetTime() - _sendTime;
		_ping = diffTime.ToString();
	}

	private int GetTime() => System.DateTime.Now.Millisecond;
}