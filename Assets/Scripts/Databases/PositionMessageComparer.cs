using System.Collections.Generic;

public class PositionMessageComparer : IEqualityComparer<Position_Messages>
{
	public bool Equals(Position_Messages b1, Position_Messages b2)
	{
		if (b2.IsNull() && b1.IsNull())
			return true;
		else if (b1.IsNull() || b2.IsNull())
			return false;
		else return b1.PositionMessage == b2.PositionMessage
				&& b1.RotationMessage == b2.RotationMessage
				&& b1.Id() == b2.Id()
				&& b1.MessageId == b2.MessageId;
	}

	public int GetHashCode(Position_Messages obj)
	{
		return (int)((obj.PositionMessage.magnitude + obj.RotationMessage.magnitude) * obj.MessageId * obj.Id());
	}
}