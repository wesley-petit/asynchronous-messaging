using System;

// It's much easier to read an error or a warning message
public enum LogType { NORMAL, WARNING, ERROR }

// Manage the log writing with time, logger type...
public static class Logger
{
	public static void Write(string log, LogType loggerType = LogType.NORMAL)
	{
		// In case of an error or warning, it will be in the message
		switch(loggerType)
		{
			case LogType.WARNING:
			case LogType.ERROR:
				log = $"{loggerType} {log}";
				break;

			default:
				break;
		}

		// Add actual time and escape string
		log = $"[{GetHorodatage()}] {log} \n";
		FileManagement.Append(FileNameConst.LOGS, log);
	}

	public static string GetHorodatage() => DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss");
}