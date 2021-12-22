using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Scream.UniMO.Utils
{
	/// <summary>
	/// This is a easy implement for multiscene loader.
	/// </summary>	
	public class SceneLoader : Common.TMonoSingleton<SceneLoader>
	{
		/// <summary>
		/// Load all the scenes in async all scene are loaded in additive mode
		/// </summary>
		/// <param name="onFinish">callback when finish</param>
		/// <param name="onProgress">return the progress of this action</param>
		/// <param name="scenes">the scenes you want to load</param>
		public IEnumerator LoadScenesAsync(Action onFinish = null, Action<float> onProgress = null, params string[] scenes)
		{
			var totalProgress = (float)scenes.Length;
			var coroutines = new List<Coroutine>();
			var operations = new List<AsyncOperation>();
			foreach (var scene in scenes)
			{
				void onOperationRequest(AsyncOperation operation)
				{
					operations.Add(operation);
				}

				var coroutine = StartCoroutine(LoadSceneAsync(scene, LoadSceneMode.Additive, onOperationRequest));
				coroutines.Add(coroutine);
			}
			foreach (var routine in coroutines)
			{
				var currentProgress = 0f;
				foreach (var operation in operations)
				{
					currentProgress += operation.progress;
				}
				onProgress?.Invoke(currentProgress / totalProgress);
				yield return routine;
			}
			onFinish?.Invoke();
		}

		/// <summary>
		/// Load all the scenes in async. And will load the main scene first. Other scene will be loaded in additive mode
		/// </summary>
		/// <param name="mainScene">name of main scene</param>
		/// <param name="onFinish">callback when finish</param>
		/// <param name="onProgress">return the progress of this action</param>
		/// <param name="scenes">the scenes you want to load in additive</param>
		/// <returns></returns>
		public IEnumerator LoadScenesAsync(string mainScene, Action onFinish = null, Action<float> onProgress = null, params string[] scenes)
		{
			var totalProgress = scenes.Length + 1f;
			var coroutines = new List<Coroutine>();
			var operations = new List<AsyncOperation>();


			//Load main scene at single mode first
			AsyncOperation mainOperation = null;

			void onGetMainOperation(AsyncOperation operation)
			{
				mainOperation = operation;
			}

			var mainRoutine = StartCoroutine(LoadSceneAsync(mainScene, LoadSceneMode.Additive, onGetMainOperation));


			//Wait for main scene loaded
			while (!mainOperation.isDone)
			{
				onProgress?.Invoke(mainOperation.progress / totalProgress);
				yield return mainRoutine;
			}

			void onTotalProgress(float progress)
			{
				onProgress?.Invoke((progress + 1f) / totalProgress);
			}

			//Load other scene parallel in additive mode
			yield return LoadScenesAsync(null, onTotalProgress, scenes);

			onFinish?.Invoke();
		}

		private IEnumerator LoadSceneAsync(string scene, LoadSceneMode mode, Action<AsyncOperation> onOperationRequest)
		{
			var operation = SceneManager.LoadSceneAsync(scene, mode);
			onOperationRequest?.Invoke(operation);

			while (!operation.isDone)
			{
				yield return null;
			}
		}
	}
}