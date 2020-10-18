using UnityEngine;

// Give Access in data
public class DatabaseController : MonoBehaviour
{
	[SerializeField] private Database _mainDatabase = new Database();

	private void Awake()
	{
		LoadDatabase();
	}

	[ContextMenu("Save Database")]
	public void SaveDatabase()
	{
		string jsonData = JsonUtility.ToJson(_mainDatabase, true);
		FileManagement.Write(FileNameConst.DATABASE, jsonData);
	}

	[ContextMenu("Load Database")]
	public void LoadDatabase()
	{
		string jsonData = FileManagement.Read(FileNameConst.DATABASE);
		if (jsonData == "")
		{
			return;
		}

		_mainDatabase = JsonUtility.FromJson<Database>(jsonData);
	}
}
