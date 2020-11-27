using UnityEngine;

// Save Metrics and Save database
public class Metrics : MonoBehaviour
{
	[SerializeField] private float _saveTime = 30f;

	private ServerManager Server => ServerManager.Instance;

	private float _time = 0f;

	private void Update()
	{
		_time += Time.deltaTime;

		if (_saveTime < _time)
		{
			_time = 0f;

			SaveMetrics();
		}
	}

	private void SaveMetrics()
	{
		Logger.Write($"Client Number : {Server.ConnectedClientsNumber}", LogType.METRICS);
		Server.ClearDoublons();
		Server.SaveDatabase();
	}
}
