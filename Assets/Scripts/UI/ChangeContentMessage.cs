using UnityEngine;
using TMPro;

// Messages Premades add a message in Content input
public class ChangeContentMessage : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _buttonText = null;

	private TMP_InputField _contentInput = null;
	private string _addValue = "";

	public void Initialize(TMP_InputField contentInput, string addValue)
	{
		if (!_buttonText)
			return;

		_contentInput = contentInput;
		_addValue = addValue;
		_buttonText.text = addValue;
		name = addValue;
	}

	public void ChangeContent()
	{
		if (_contentInput)
		{
			_contentInput.text += $" {_addValue}";
		}
	}
}
