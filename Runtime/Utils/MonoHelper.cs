using System;

namespace Scream.UniMO.Utils
{
	/// <summary>
	/// this class is inherit from MonoBehaviour 
	/// you can subscribe for mono event use this class
	/// even if your class isn't inherit from  MonoBehaviour
	/// </summary>
	public class MonoHelper : Common.TMonoSingleton<MonoHelper>
	{
		public event Action OnAwakeAction;
		public event Action OnEnableAction;
		public event Action OnStartAction;
		public event Action OnUpdateAction;
		public event Action OnFixedUpdateAction;
		public event Action OnLateUpdateAction;
		public event Action OnDisableAction;
		public event Action OnDestroyAction;
		public event Action OnApplicationQuitAction;

		private void Awake()
		{
			OnAwakeAction?.Invoke();
		}

		private void OnEnable()
		{
			OnEnableAction?.Invoke();
		}

		private void Start()
		{
			OnStartAction?.Invoke();
		}

		private void Update()
		{
			OnUpdateAction?.Invoke();
		}

		private void FixedUpdate()
		{
			OnFixedUpdateAction?.Invoke();
		}

		private void LateUpdate()
		{
			OnLateUpdateAction?.Invoke();
		}

		private void OnDisable()
		{
			OnDisableAction?.Invoke();
		}

		private void OnDestroy()
		{
			OnDestroyAction?.Invoke();
		}

		private void OnApplicationQuit()
		{
			OnApplicationQuitAction?.Invoke();
		}


	}
}