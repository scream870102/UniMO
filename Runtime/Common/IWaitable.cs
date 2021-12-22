using System;
using System.Collections;
using System.Threading.Tasks;

namespace Scream.UniMO.Common
{
	/// <summary>
	/// Define if an action can wait as coroutine or async method
	/// </summary>
	interface IWaitable
	{
		/// <summary>
		/// Define action is ready or not
		/// </summary>
		bool IsReady { get; }

		/// <summary>
		/// Wait an action in Coroutine
		/// </summary>
		/// <param name="onFinish">Pass the method to catch the result</param>
		IEnumerator Wait(Action onFinish);

		/// <summary>
		/// Wait an action as async method
		/// </summary>
		/// <returns>Result</returns>
		Task Wait();
	}

	/// <summary>
	/// Define if an action can wait as coroutine or async method
	/// </summary>
	/// <typeparam name="T">Result type</typeparam>
	interface IWaitable<T>
	{
		/// <summary>
		/// Define action is ready or not
		/// </summary>
		bool IsReady { get; }

		/// <summary>
		/// Wait an action in Coroutine
		/// </summary>
		/// <param name="onFinish">Pass the method to catch the result</param>
		IEnumerator Wait(Action<T> onFinish);

		/// <summary>
		/// Wait an action as async method
		/// </summary>
		/// <returns>Result</returns>
		Task<T> Wait();
	}

	/// <summary>
	/// A waitable action like counter
	/// </summary>
	/// <typeparam name="T">Result type</typeparam>
	interface ICountWaitable<T> : IWaitable<T>
	{
		/// <summary>
		/// How many steps remain
		/// </summary>
		int Remain { get; }

		/// <summary>
		/// Add more step to this counter
		/// </summary>
		/// <param name="step">how many steps to add</param>
		void Add(int step = 1);

		/// <summary>
		/// Set a step is already finish
		/// </summary>
		void SetDone();
	}

	/// <summary>
	/// interface which inherit from IWaitable
	/// </summary>
	interface ICountWaitable : IWaitable
	{
		/// <summary>
		/// How many steps remain
		/// </summary>
		int Remain { get; }

		/// <summary>
		/// Add more step to this counter
		/// </summary>
		/// <param name="step">how many steps to add</param>
		void Add(int step = 1);

		/// <summary>
		/// Set a step is already finish
		/// </summary>
		void SetDone();
	}
}