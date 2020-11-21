using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LogWindow : EditorWindow
{
	// Content
	private List<Log> _logsToDiplay = new List<Log>();
	private Dictionary<LogType, List<Log>> _logsByType = new Dictionary<LogType, List<Log>>();

	// Style
	private ConfigColor _configColor = null;
	private Vector2 _scrollPosition = Vector2.zero;
	private RectOffset _boxPadding = new RectOffset();
	private RectOffset _boxMargin = new RectOffset();

	[MenuItem("Window/LogWindow")]
	public static void OpenWindow()
	{
		GetWindow<LogWindow>("Log Inspector");
	}

	private void OnFocus() => Initialize();

	private void OnGUI()
	{
		Header();
		ConfigColor t1 = Resources.LoadAll<ConfigColor>("LogsColors")[0];

		if (!_configColor)
		{
			Debug.LogError("Config Color is undefined.");
			return;
		}

		Content();
	}

	//ConfigColor t1 = Resources.LoadAll<ConfigColor>("LogsColors")[0];
	private void Initialize()
	{
		_boxPadding = new RectOffset(5, 5, 10, 10);
		LoadLog();
		SearchConfigColor();
	}

	private void LoadLog()
	{
		_logsToDiplay.Clear();
		_logsByType.Clear();

		string[] content = FileManagement.ReadAllLines(FileNameConst.LOGS);

		// Add a type for each line
		foreach (var line in content)
		{
			// Default type
			LogType type = LogType.NORMAL;

			// Change the type, if it has a different type
			foreach (string name in System.Enum.GetNames(typeof(LogType)))
			{
				if (line.Contains(name))
				{
					type = (LogType)System.Enum.Parse(typeof(LogType), name);
					break;
				}
			}
			Log lineLog = new Log(line, type);
			_logsToDiplay.Add(lineLog);

			// Initialize
			if (!_logsByType.ContainsKey(type))
			{
				_logsByType[type] = new List<Log>();
			}
			_logsByType[type].Add(lineLog);
		}
	}

	private void SearchConfigColor()
	{
		ConfigColor[] searchColor = Resources.LoadAll<ConfigColor>("LogsColors");

		if (0 < searchColor.Length)
		{
			_configColor = searchColor[0];
		}
	}

	// Display filter buttons
	private void Header()
	{
		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button($"Refesh {_logsToDiplay.Count}"))
		{
			LoadLog();
		}

		// Display a filter for each type
		foreach (var key in _logsByType)
		{
			if (GUILayout.Button($"{key.Key} {key.Value.Count}"))
			{
				_logsToDiplay = _logsByType[key.Key];
			}
		}

		EditorGUILayout.EndHorizontal();
	}

	// Display Logs
	private void Content()
	{
		_scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, true);
		
		foreach (var oneLog in _logsToDiplay)
		{
			GUI.backgroundColor = _configColor.GetColorByLogType(oneLog.Type);
			Color textColor = _configColor.GetTextColorByLogType(oneLog.Type);

			GUILayout.Box(oneLog.Text, GetBoxStyle(textColor));
		}

		GUILayout.EndScrollView();
	}

	private GUIStyle GetBoxStyle(Color textColor)
	{
		GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		boxStyle.stretchWidth = true;
		boxStyle.alignment = TextAnchor.UpperLeft;
		boxStyle.padding = _boxPadding;
		boxStyle.margin = _boxMargin;
		boxStyle.normal.textColor = textColor;

		DeleteBorder(boxStyle);
		return boxStyle;
	}

	private void DeleteBorder(GUIStyle boxStyle)
	{
		Color[] pix = new Color[] { Color.white };
		Texture2D result = new Texture2D(1, 1);
		result.SetPixels(pix);
		result.Apply();
		boxStyle.normal.background = result;
	}
}