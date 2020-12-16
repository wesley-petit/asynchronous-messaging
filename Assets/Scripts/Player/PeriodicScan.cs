using System.Collections;
using UnityEngine;

public class PeriodicScan : MonoBehaviour
{
	public Transform _playerPos;
	[SerializeField] private Vector3 _oldPlayerPos;
	[SerializeField] private int _distanceUntilUpdate = 0;
	[SerializeField] private DisplayMessages display = null;


	// Start is called before the first frame update
	void Start()
	{
		_oldPlayerPos = _playerPos.position;
		UIManager.Instance.OnScanMessage += OnMessageReceived;
		StartCoroutine(Scan());
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		
	}

	void UpdatePosition()
	{
		UIManager.Instance.SendScanMessage(_playerPos.position);
		_oldPlayerPos = _playerPos.position;
	}

	void OnMessageReceived(Message[] messages)
	{
		display.DisplayMessagesFromList(messages);
	}

	IEnumerator Scan()
    {
		while (true)
		{
			yield return new WaitUntil(() => Vector3.Distance(_oldPlayerPos, _playerPos.position) > _distanceUntilUpdate);
			UIManager.Instance.SendScanMessage(_playerPos.position);
			_oldPlayerPos = _playerPos.position;
		}
	}
}
