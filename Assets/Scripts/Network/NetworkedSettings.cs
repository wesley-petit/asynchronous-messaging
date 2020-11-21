using MLAPI.NetworkedVar;

// Share permission and tickrate in all file
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

	public static NetworkedVarSettings OwnerOnly = new NetworkedVarSettings()
	{
		ReadPermission = NetworkedVarPermission.OwnerOnly,
		WritePermission = NetworkedVarPermission.OwnerOnly,
		SendTickrate = 0
	};

	public static NetworkedVarSettings ServerRead = new NetworkedVarSettings()
	{
		ReadPermission = NetworkedVarPermission.ServerOnly,
		WritePermission = NetworkedVarPermission.OwnerOnly,
		SendTickrate = 0
	};

	public static NetworkedVarSettings OwnerRead = new NetworkedVarSettings()
	{
		ReadPermission = NetworkedVarPermission.OwnerOnly,
		WritePermission = NetworkedVarPermission.ServerOnly,
		SendTickrate = 0
	};
}
