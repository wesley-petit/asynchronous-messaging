using UnityEngine;

public class ServerManager : MonoBehaviour
{
    public static ServerManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"There is two Singleton of the same type {typeof(ServerManager)}");
            Destroy(gameObject);
        }
	}

    // If it's receive Hello, World !, he sends Good Bye
    public string RequestHelloWorld(string content)
    {
        if (content == "Hello, World ! ")
        {
            return "Good Bye";
        }

        return "No Connection";
    }
}