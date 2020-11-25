using MLAPI;
using UnityEngine;

public class DummyClient : MonoBehaviour
{
	private void Start()
	{
		NetworkingManager.Singleton.StartClient();
	}
}
