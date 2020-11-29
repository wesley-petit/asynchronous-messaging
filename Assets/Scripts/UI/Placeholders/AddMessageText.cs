using UnityEngine;

public class AddMessageText : MonoBehaviour
{
	public string Content { get; set; } = "";

	[SerializeField] private Transform position;

	public void AddMessage()
	{
		if (string.IsNullOrWhiteSpace(Content))
			return;

		UIManager.Instance.SendAddMessage(Content, position.position);
	}
}
