using UnityEngine;
using TMPro;

public class PingText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _text = null;

	private void Start() => UIManager.Instance.OnPing += DisplayPing;

	private void OnDisable() => UIManager.Instance.OnPing -= DisplayPing;

	private void DisplayPing(string pingValue) => _text.text = $"{pingValue} ms";
}
