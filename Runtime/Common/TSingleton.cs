namespace Scream.UniMO.Common
{
	/// <summary>
	/// derived this class to make a class into singleton class
	/// </summary>
	/// <typeparam name="T">the class you want to make it singleton makesure it has a no parameter constructor</typeparam>
	public class TSingleton<T> where T : class, new()
	{
		private static T instance = null;

		/// <summary>
		/// Get the instance of this class
		/// </summary>
		public static T Instance => GetInstance();

		private static T GetInstance()
		{
			if (instance == null)
			{
				instance = new T();
			}
			return instance;
		}
	}
}