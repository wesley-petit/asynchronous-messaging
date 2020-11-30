using UnityEngine;
using TMPro;

public class ScanMessageText : MonoBehaviour
{
	[SerializeField] private StringToVector3 _stringToVector3 = null;
	[SerializeField] private TextMeshProUGUI _textClone = null;

	private UIManager UI => UIManager.Instance;

	private TextMeshProUGUI[] _clones = new TextMeshProUGUI[0];

	private void Start() => UI.OnScanMessage += DisplayMessages;

	private void OnDisable() => UI.OnScanMessage -= DisplayMessages;

	public void ScanMessage()
	{
		if (!_stringToVector3)
			return;

		UI.SendScanMessage(_stringToVector3.GetPosition);
	}

	#region DisplayMessages
	private void DisplayMessages(Message[] messages)
	{
		DestroyClones();

		_clones = new TextMeshProUGUI[messages.Length];

		CreateClones(messages);
	}

	private void CreateClones(Message[] messages)
	{
		for (int i = 0; i < messages.Length; i++)
		{
			TextMeshProUGUI cloneText = Instantiate(_textClone, transform);
			cloneText.text = messages[i].GetMessageContent;
			_clones[i] = cloneText;
		}
	}

	private void DestroyClones()
	{
		if (0 < _clones.Length)
		{
			foreach (var clone in _clones)
			{
				Destroy(clone.gameObject);
			}
		}
	}
	#endregion
}
