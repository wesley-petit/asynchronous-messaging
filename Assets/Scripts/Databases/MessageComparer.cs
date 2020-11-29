using System.Collections.Generic;

// Use in a HashSet to eliminate doublons
public class MessageComparer : IEqualityComparer<Message>
{
	public bool Equals(Message b1, Message b2)
	{
		if (b1.ContainsNullValues && b2.ContainsNullValues)
			return true;
		else if (b1.IsEmpty && b2.IsEmpty)
			return true;
		else if (b1.IsEmpty || b2.IsEmpty)
			return false;
		else if (b1.ContainsNullValues || b2.ContainsNullValues)
			return false;
		else return b1.GetUserName == b2.GetUserName
				&& b1.GetMessageContent == b2.GetMessageContent
				&& b1.GetMessageTime == b2.GetMessageTime
				&& b1.GetPositionMessage == b2.GetPositionMessage;
	}

	public int GetHashCode(Message obj) => (int)obj.GetPositionMessage.magnitude;
}