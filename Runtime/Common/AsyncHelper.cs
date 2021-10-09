using System;
using System.Collections;
using System.Threading.Tasks;

namespace Scream.UniMO.Common
{
	/// <summary>
	/// Listen for event and wait until the event callback invoke as IEnumerator or Async method
	/// </summary>
	/// <typeparam name="Input">The type for request parameter</typeparam>
	/// <typeparam name="Result">The result type for callback</typeparam>
	public class AsyncHelper<Input, Result> : IWaitable<Result>
	{
		/// <summary>
		/// Return if this action already finish
		/// </summary>
		public bool IsReady { get; private set; }
		private Result result;
		private Action<Input> request;

		public AsyncHelper(Action<Input> request)
		{
			this.request = request;
		}

		/// <summary>
		/// Call this method to try to get the method at anytime.
		/// It won't return the result if callback is not invoke
		/// </summary>
		/// <param name="result">result</param>
		/// <returns>if callback is already invoke</returns>
		public bool TryGetResult(out Result result)
		{
			result = default;
			if (IsReady)
			{
				result = this.result;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Use this method to register the event you want to wait
		/// </summary>
		/// <param name="result">result</param>
		public void Callback(Result result)
		{
			this.result = result;
			IsReady = true;
		}

		/// <summary>
		/// Raise a request
		/// </summary>
		/// <param name="data">the input data</param>
		public void Request(Input data)
		{
			request?.Invoke(data);
		}

		/// <summary>
		/// Call this to wait in coroutine
		/// </summary>
		/// <param name="onFinish">when callback is invoke you can use this method to catch the result</param>
		public IEnumerator Wait(Action<Result> onFinish = null)
		{
			while (!IsReady)
			{
				yield return null;
			}
			onFinish?.Invoke(result);
		}

		/// <summary>
		/// Call this to wait as async method
		/// </summary>
		/// <returns>the result</returns>
		public async Task<Result> Wait()
		{
			while (!IsReady)
			{
				await Task.Yield();
			}
			return result;
		}
	}
}
