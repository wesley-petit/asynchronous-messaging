using MLAPI;
using UnityEngine;

public class LaunchClient : MonoBehaviour
{
	private void Start() => NetworkingManager.Singleton.StartClient();
}
