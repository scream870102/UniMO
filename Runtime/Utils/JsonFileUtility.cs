using System.IO;
using UnityEngine;

namespace Scream.UniMO.Utils
{
    public class JsonFileUtility
    {
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
