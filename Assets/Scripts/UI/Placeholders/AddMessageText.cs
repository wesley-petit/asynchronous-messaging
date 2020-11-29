using UnityEngine;

public class AddMessageText : MonoBehaviour
{
	public string Content { get; set; } = "";

	[SerializeField] private StringToVector3 _stringToVector3 = null;

	public void AddMessage()
	{
		if (string.IsNullOrWhiteSpace(Content))
			return;

		if (!_stringToVector3)
			return;

		UIManager.Instance.SendAddMessage(Content, _stringToVector3.GetPosition);
	}
}
