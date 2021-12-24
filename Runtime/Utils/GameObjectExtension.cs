using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scream.UniMO.Utils
{
	/// <summary>
	/// Extension method for Unity basic object.
	/// </summary>
	public static class GameObjectExtension
	{
		/// <summary>
		/// GetComponet and will print log in error when can't find component
		/// </summary>
		/// <typeparam name="T">Type of component</typeparam>
		/// <returns>return the component. Will return null when can't find the component</returns>
		public static T GetComponentLog<T>(this GameObject gameObject) where T : Component
		{
			var result = gameObject.GetComponent<T>();
#if UNITY_EDITOR
			PrintLog(gameObject, result);
#endif
			return result;
		}

		/// <summary>
		/// GetComponet and will print log in error when can't find component
		/// </summary>
		/// <param name="component">Type of component</param>
		/// <returns>return the component. Will return null when can't find the component</returns>
		public static T GetComponentLog<T>(this Component component) where T : Component
		{
			var result = component.GetComponent<T>();
#if UNITY_EDITOR
			PrintLog(component, result);
#endif
			return result;
		}

#if UNITY_EDITOR
		private static void PrintLog<T>(Object obj, T result) where T : Component
		{
			if (result == null)
			{
				var type = typeof(T);
				Debug.LogError($"Get Component type of \"{type}\" failed in \"{obj.name}\"\n{Environment.StackTrace}");
			}
		}
#endif

	}
}