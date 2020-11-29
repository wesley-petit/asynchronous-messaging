using UnityEngine;
using TMPro;

public class MessagesPremadesText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _textClone = null;

	private TextMeshProUGUI[] _clones = new TextMeshProUGUI[0];

	private void Start() => UIManager.Instance.OnMessagePremade += DisplayMessagesPremades;

	private void OnDisable() => UIManager.Instance.OnMessagePremade -= DisplayMessagesPremades;

	#region DisplayMessagesPremades
	private void DisplayMessagesPremades(string[] messagesPremades)
	{
		DestroyClones();

		_clones = new TextMeshProUGUI[messagesPremades.Length];

		CreateClones(messagesPremades);
	}

	private void CreateClones(string[] messagesPremades)
	{
		for (int i = 0; i < messagesPremades.Length; i++)
		{
			TextMeshProUGUI cloneText = Instantiate(_textClone, transform);
			cloneText.text = messagesPremades[i];
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
