using UnityEngine;
using System.Text;
using System;

namespace Scream.UniMO.Common
{
	/// <summary>
	/// Common logger append with time and stack 
	/// log will only show in editor if use method which prefix with "Editor"
	/// </summary>
	public static class Logger
	{
		private static readonly StringBuilder builder = new();

		public static void EditorLog(string message)
		{
#if UNITY_EDITOR
			Debug.Log(ConcatLog(message));
#endif
		}

		public static void EditorWarning(string message)
		{
#if UNITY_EDITOR
			Debug.LogWarning(ConcatLog(message));
#endif
		}

		public static void EditorError(string message)
		{
#if UNITY_EDITOR
			Debug.LogError(ConcatLog(message));
#endif
		}
		public static void Log(string message)
		{
			Debug.Log(ConcatLog(message));
		}

		public static void Warning(string message)
		{
			Debug.LogWarning(ConcatLog(message));
		}

		public static void Error(string message)
		{
			Debug.LogError(ConcatLog(message));
		}

		private static string ConcatLog(string message)
		{
			var currentTime = DateTime.Now.ToLocalTime();
			builder.Clear();
			builder.Append($"[{currentTime:H:mm:ss}] {message}\n{ Environment.StackTrace}");
			return builder.ToString();
		}
	}
}