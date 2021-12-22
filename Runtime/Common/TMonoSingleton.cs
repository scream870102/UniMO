using UnityEngine;
namespace Scream.UniMO.Common
{
	/// <summary>
	/// derived this class to make a class into singleton class which won't be destroyed when scene change
	/// </summary>
	/// <typeparam name="T">the class you want to make it as singleton class must derived from monoBehaviour</typeparam>
	public class TMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		/// <summary>
		/// Get the instance of this class
		/// </summary>
		public static T Instance => GetInstance();
		private static T instance = null;
		private static T GetInstance()
		{
			if (instance == null)
			{
				var type = typeof(T);
				var gameObject = new GameObject(type.Name);
				instance = gameObject.AddComponent<T>();
				DontDestroyOnLoad(gameObject);
			}
			return instance;
		}

	}
}