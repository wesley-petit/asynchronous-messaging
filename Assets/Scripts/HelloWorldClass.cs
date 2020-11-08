using UnityEngine;
using MLAPI.NetworkedVar;
using MLAPI;

public class HelloWorldClass : NetworkedBehaviour
{
    public NetworkedVar<string> Value = new NetworkedVar<string>(new NetworkedVarSettings()
    {
        ReadPermission = NetworkedVarPermission.Everyone,
        WritePermission = NetworkedVarPermission.Everyone,
        SendTickrate = 0
    }, "");

    [ContextMenu("HelloWorld")]
    public void HelloWorld()
    {
        Value.Value += "Hello, World ! ";
    }
}
