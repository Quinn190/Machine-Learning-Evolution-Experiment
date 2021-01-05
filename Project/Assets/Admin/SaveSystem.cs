using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

	public static void SaveState(GameEngine GE)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = @"C:\Users\Danny\Documents\Saturn Data";
		FileStream stream = new FileStream(path, FileMode.Create);

		Data data = new Data(GE);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static Data LoadState () 
	{
		string path = Application.persistentDataPath + "/BestSaves.save";
		if (File.Exists(path)) 
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			Data data = formatter.Deserialize(stream) as Data;
			stream.Close();
			return data;


		} else
		{
			Debug.Log("Load ErroR");
			return null;
		}
	}
}
