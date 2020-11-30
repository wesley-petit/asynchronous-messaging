using MLAPI;
using UnityEngine;

public class LaunchClient : MonoBehaviour
{
	public void StartClient() => NetworkingManager.Singleton.StartClient();
}
