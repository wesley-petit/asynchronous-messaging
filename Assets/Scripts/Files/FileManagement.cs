using UnityEngine;
using System.IO;
using System;

// Read and Write
public static class FileManagement
{
	public static string Read(string fileName)
	{
		try
		{
			string path = GetFilePath(fileName);
			Logger.Write($"Read File {path}");

			if (!File.Exists(path))
			{
				Logger.Write($"File not found at {path}", LogType.ERROR);
				return "";
			}

			return File.ReadAllText(path);
		}
		catch (Exception e)
		{
			Logger.Write(e.ToString(), LogType.ERROR);
			return "";
		}
	}

	public static void Write(string fileName, string content)
	{
		try
		{
			string path = GetFilePath(fileName);
			Logger.Write($"Write File {path}");

			if (!File.Exists(path))
			{
				Logger.Write($"File not found at {path}", LogType.ERROR);
				return;
			}

			File.WriteAllText(path, content);
		}
		catch (Exception e)
		{
			Logger.Write(e.ToString(), LogType.ERROR);
		}
	}

	// Use to add a log line
	public static void Append(string fileName, string content)
	{
		try
		{
			string path = GetFilePath(fileName);
			if (!File.Exists(path))
			{
				Debug.LogWarning($"File not found at {path}");
				return;
			}

			File.AppendAllText(path, content);
		}
		catch (Exception e)
		{
			Debug.LogError(e.ToString());
		}
	}

	private static string GetFilePath(string fileName) => Path.Combine(Application.dataPath, fileName);
}
