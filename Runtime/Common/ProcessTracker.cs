using System;
using System.Collections;
using System.Threading.Tasks;

namespace Scream.UniMO.Common
{
	/// <summary>
	/// Use this class to track a series of action is finish or not
	/// </summary>
	public class ProcessTracker : ICountWaitable
	{
		/// <summary>
		/// The total progression of the work
		/// </summary>
		public float Percentage
		{
			get
			{
				var result = (float)currentSteps / maxSteps;
				if (result > 1f)
				{
					return 1f;
				}
				return result;
			}
		}

		/// <summary>
		/// How many steps remain
		/// </summary>
		public int Remain => maxSteps - currentSteps;

		/// <summary>
		/// Define action is ready or not
		/// </summary>
		public bool IsReady => currentSteps >= maxSteps;

		private readonly Action onFinishAction;

		private int maxSteps;

		private int currentSteps;

		public ProcessTracker(int steps, Action onFinishAction = null)
		{
			maxSteps = steps;
			currentSteps = 0;
			this.onFinishAction = onFinishAction;
		}

		/// <summary>
		/// Add more step to this counter
		/// </summary>
		/// <param name="step">how many steps to add</param>
		public void Add(int step = 1)
		{
			maxSteps += step;
		}

		/// <summary>
		/// Set a step is already finish
		/// </summary>
		public void SetDone()
		{
			currentSteps++;
			if (IsReady && currentSteps == maxSteps)
			{
				onFinishAction?.Invoke();
			}
		}

		/// <summary>
		/// Wait an action in Coroutine
		/// </summary>
		/// <param name="onFinish">Pass the method to catch the result</param>
		public IEnumerator Wait(Action onFinish = null)
		{
			while (!IsReady)
			{
				yield return null;
			}
			onFinish?.Invoke();
		}

		/// <summary>
		/// Wait an action as async method
		/// </summary>
		/// <returns>Result</returns>
		public async Task Wait()
		{
			while (!IsReady)
			{
				await Task.Yield();
			}
		}

		/// <summary>
		/// Reset this action to start
		/// </summary>
		public void Reset()
		{
			currentSteps = 0;
		}

	}
}