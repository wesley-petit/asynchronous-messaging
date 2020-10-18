using UnityEngine;
using System.Linq;
using System.Collections.Generic;

// Give Access in data
public class DatabaseController : MonoBehaviour
{
	[SerializeField] private Database _mainDatabase = new Database();

	private void Awake()
	{
		LoadDatabase();
	}


	public bool IsOnTheList<TSource>(IEnumerable<TSource> searchList, System.Func<TSource, bool> predicate) => searchList.Any(predicate);

	public TSource GetElementByPredicate<TSource>(IEnumerable<TSource> searchList, System.Func<TSource, bool> predicate) => searchList.Where(predicate).FirstOrDefault();

	[ContextMenu("Save Database")]
	public void SaveDatabase()
	{
		Logger.Write("Save Database...");
		string jsonData = JsonUtility.ToJson(_mainDatabase, true);
		FileManagement.Write(FileNameConst.DATABASE, jsonData);
	}

	[ContextMenu("Load Database")]
	public void LoadDatabase()
	{
		Logger.Write("Load Database...");
		string jsonData = FileManagement.Read(FileNameConst.DATABASE);
		if (jsonData == "")
		{
			Logger.Write("Failed to Load Database...", LogType.ERROR);
			return;
		}

		_mainDatabase = JsonUtility.FromJson<Database>(jsonData);
	}
}
