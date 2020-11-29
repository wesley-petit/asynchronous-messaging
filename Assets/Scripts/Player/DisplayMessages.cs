using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessages : MonoBehaviour
{
    [SerializeField] private GameObject _messagePrefab = null;
    [SerializeField] private GameObject _parent = null;

    public void DisplayMessagesFromList(Message[] messageList)
    {
        foreach(Transform child in _parent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(Message message in messageList)
        {
            GameObject messageObject = Instantiate(_messagePrefab, message.GetPositionMessage, Quaternion.identity);
            messageObject.transform.SetParent(_parent.transform);
            messageObject.name = message.GetUserName + " | " + message.GetMessageTime+ " | " + message.GetMessageContent;
        }
    }
}
