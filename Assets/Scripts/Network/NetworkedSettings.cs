using MLAPI.NetworkedVar;

// Share permission and tickrate in a ll file
public static class NetworkedSettings
{
	public static NetworkedVarSettings Everyone = new NetworkedVarSettings()
	{
		ReadPermission = NetworkedVarPermission.Everyone,
		WritePermission = NetworkedVarPermission.Everyone,
		SendTickrate = 0
	};

	public static NetworkedVarSettings ServerOnly = new NetworkedVarSettings()
	{
		ReadPermission = NetworkedVarPermission.ServerOnly,
		WritePermission = NetworkedVarPermission.ServerOnly,
		SendTickrate = 0
	};
}
