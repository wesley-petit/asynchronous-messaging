using UnityEngine;

// It's much easier to read an error or a warning message
// I know, there is UnityEngine.LogType
public enum LogType { NORMAL, WARNING, ERROR }

// Use in the log window
public struct Log
{
	public string Text { get; private set; }
	public LogType Type { get; private set; }

	public Log(string text, LogType type)
	{
		Text = text;
		Type = type;
	}
}

[System.Serializable]
public struct ColorByLogType
{
	[SerializeField] private LogType _type;
	[SerializeField] private Color[] _colors;

	public LogType Type => _type;
	public Color[] Colors => _colors;
}
