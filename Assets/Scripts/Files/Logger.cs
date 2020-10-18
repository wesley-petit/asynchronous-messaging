using System;

// It's much easier to read an error or a warning message
public enum LoggerType { NORMAL, WARNING, ERROR }

// Manage the log writing with time, logger type...
public static class Logger
{
	public static void Write(string log, LoggerType loggerType = LoggerType.NORMAL)
	{
		// In case of an error or warning, it will be in the message
		switch(loggerType)
		{
			case LoggerType.WARNING:
			case LoggerType.ERROR:
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