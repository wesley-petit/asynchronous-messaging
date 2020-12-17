using System.Collections.Generic;
using UnityEngine;

public class DisplayMessages : MonoBehaviour
{
	[SerializeField] private Dialog _messagePrefab = null;
	[SerializeField] private GameObject _parent = null;
	private List<string> _list = new List<string>();

	public void DisplayMessagesFromList(Message[] messageList)
	{
		foreach (Message message in messageList)
		{
			if (!_list.Contains(message.GetUserName + " | " + message.GetMessageTime + " | " + message.GetMessageContent))
			{
				_list.Add(message.GetUserName + " | " + message.GetMessageTime + " | " + message.GetMessageContent);

				Dialog messageObject = Instantiate(_messagePrefab, message.GetPositionMessage, Quaternion.identity);
				messageObject.transform.SetParent(_parent.transform);
				messageObject.name = message.GetUserName + " | " + message.GetMessageTime + " | " + message.GetMessageContent;
				messageObject.SetDialog(message);
			}
		}
	}
}
