using UnityEngine;
using System.IO;
using System;

// Read and Write
public static class FileManagement
{
	#region Read
	public static string Read(string fileName)
	{
		try
		{
			string path = GetFilePath(fileName);
			Logger.Write($"Read File {path}");

			if (!File.Exists(path))
			{
				File.Create(path);
				Logger.Write($"File not found at {path}", LogType.WARNING);
			}

			return File.ReadAllText(path);
		}
		catch (Exception e)
		{
			Logger.Write(e.ToString(), LogType.ERROR);
			return "";
		}
	}

	public static string[] ReadAllLines(string fileName)
	{
		try
		{
			string path = GetFilePath(fileName);
			if (!File.Exists(path))
			{
				File.Create(path);
				Debug.LogWarning($"File not found at {path}, so we create it");
			}

			return File.ReadAllLines(path);
		}
		catch (Exception e)
		{
			Debug.LogError(e);
			return new string[0];
		}
	}
	#endregion

	#region Write
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

	// Use to add a log line / Create a file if it doesn't exist
	public static void Append(string fileName, string content)
	{
		try
		{
			string path = GetFilePath(fileName);
			if (!File.Exists(path))
			{
				Debug.LogWarning($"File not found at {path}. Let there be a file.");
			}

			File.AppendAllText(path, content);
		}
		catch (Exception e)
		{
			Debug.LogError(e.ToString());
		}
	}
	#endregion

	// File Path in production or in release
	private static string GetFilePath(string fileName)
	{
#if UNITY_EDITOR
		return Path.Combine(Application.dataPath, fileName);
#else
		return Path.Combine(Application.persistentDataPath, fileName);
#endif
	}
}
