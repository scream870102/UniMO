using System.IO;
using UnityEngine;

namespace Scream.UniMO.Utils
{
	/// <summary>
	/// Utils about json file
	/// </summary>
	public class JsonFileUtility
	{
		/// <summary>
		/// Create a json file to specific path by custom object
		/// </summary>
		/// <param name="obj">object wants to convert to json file</param>
		/// <param name="path">path for json file</param>
		/// <param name="fileName">file name for json file</param>
		/// <typeparam name="T">type of original object</typeparam>
		static public void CreateJsonFile<T>(T obj, string path, string fileName)
		{
			CheckDirectory(path);
			var fullPath = Path.Combine(path, fileName);
			if (File.Exists(fullPath))
			{
				Debug.Log($"{fullPath} already exists");
				return;
			}
			var fs = new FileStream(fullPath, FileMode.Create);
			string fileContext = JsonUtility.ToJson(obj);
			var file = new StreamWriter(fs);
			file.Write(fileContext);
			file.Close();
		}

		/// <summary>
		/// load json file from path and convert to specific type
		/// </summary>
		/// <param name="path">path of json file</param>
		/// <typeparam name="T">type want to convert to</typeparam>
		/// <returns>result</returns>
		static public T Load<T>(string path)
		{
			if (!File.Exists(path)) return default(T);
			return JsonUtility.FromJson<T>(File.ReadAllText(path));
		}


		static void CheckDirectory(string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}
	}
}
