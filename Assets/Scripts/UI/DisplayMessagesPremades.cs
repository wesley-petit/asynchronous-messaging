using UnityEngine;
using TMPro;

// Display a list of buttons to change the content
public class DisplayMessagesPremades : MonoBehaviour
{
	[SerializeField] private ChangeContentMessage _changeContent = null;// Prefabs to clone
	[SerializeField] private TMP_InputField _contentInput = null;		// Value to change
	[SerializeField] private Transform _container = null;				// Clone container / Parent

	private ChangeContentMessage[] _clones = new ChangeContentMessage[0];

	private void Start() => UIManager.Instance.OnMessagePremade += CreateClone;

	private void OnDestroy() => UIManager.Instance.OnMessagePremade -= CreateClone;

	private void CreateClone(string[] messagesPremades)
	{
		DestroyClones();

		_clones = new ChangeContentMessage[messagesPremades.Length];

		for (int i = 0; i < messagesPremades.Length; i++)
		{
			_clones[i] = Instantiate(_changeContent, _container);
			_clones[i].Initialize(_contentInput, messagesPremades[i]);
		}
	}

	private void DestroyClones()
	{
		for (int i = 0; i < _clones.Length; i++)
		{
			Destroy(_clones[i]);
		}
	}
}
