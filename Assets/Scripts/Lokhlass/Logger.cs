using System;

// Manage the log writing with time, logger type...
public static class Logger
{
	public static void Write(string log, LogType loggerType = LogType.NORMAL)
	{
		// In case of an error or warning, it will be in the message
		if (loggerType != LogType.NORMAL)
		{
			log = $"{loggerType} {log}";
		}

		// Add actual time and escape string
		log = $"[{GetHorodatage()}] {log} \n";
		FileManagement.Append(FileNameConst.LOGS, log);
	}

	public static string GetHorodatage() => DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss");
}