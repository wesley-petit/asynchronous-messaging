using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Logs Color", menuName = "Custom/Logs Color")]
public class ConfigColor : ScriptableObject
{
	[SerializeField] private ColorByLogType[] _colorByLogType = new ColorByLogType[0];
	[SerializeField] private Color _notFoundColor = Color.black;

	private Dictionary<LogType, int> _indexByType = new Dictionary<LogType, int>();

	// Search a color for a certain type
	public Color GetColorByLogType(LogType _type)
	{
		foreach (var oneLine in _colorByLogType)
		{
			if (oneLine.Type == _type)
			{
				SwitchIndex(oneLine.Colors, oneLine.Type);
				return oneLine.Colors[_indexByType[oneLine.Type]];
			}
		}

		return _notFoundColor;
	}

	public Color GetTextColorByLogType(LogType _type)
	{
		foreach (var oneLine in _colorByLogType)
		{
			if (oneLine.Type == _type)
			{
				return oneLine.TextColor;
			}
		}

		return _notFoundColor;
	}

	// Allow a switch between multiple Color
	private void SwitchIndex(Color[] colors, LogType type)
	{
		if (!_indexByType.ContainsKey(type))
		{
			_indexByType[type] = 0;
		}
		if (colors.Length == 1)
		{
			return;
		}

		_indexByType[type]++;
		if (colors.Length <= _indexByType[type])
		{
			_indexByType[type] = 0;
		}
	}
}
