using UnityEngine;

namespace Scream.UniMO.Control
{
	/// <summary>
	/// TouchHelper is a class to help developer deal with single finger touch action in touchable device
	/// It will use mouse to simulate the touch in Editor phase
	/// </summary>
	public class TouchHelper
	{
		private const int mouseButtonIndex = 0;

		private const int mouseSimulateIndex = -1;

		private const int normalTouchIndex = 0;

		private const int nullFingerIndex = -2;

		private const float mouseTapThreshold = 0.2f;

		/// <summary>
		/// How many touch at this moment
		/// </summary>
		public static int TouchCount => Input.touchCount;

		/// <summary>
		/// the moving finger index 
		/// </summary>
		public int MoveFingerId => moveFingerId;

		private float mouseDeltaTime = 0f;

		private bool isFingerAlreadyMoved = false;

		private int moveFingerId = nullFingerIndex;


		/// <summary>
		/// Call this method event frame before you try to access other method
		/// it will update all the information
		/// </summary>
		public void Tick()
		{
			var touches = Input.touches;
			foreach (var touch in touches)
			{
				if (touch.phase == TouchPhase.Moved)
				{
					isFingerAlreadyMoved = true;
					moveFingerId = touch.fingerId;
				}
				if (touch.phase == TouchPhase.Began)
				{
					isFingerAlreadyMoved = false;
					moveFingerId = nullFingerIndex;
				}
			}
		}

		/// <summary>
		/// Check if there is any finger just down
		/// </summary>
		/// <param name="index">the touch index</param>
		/// <returns>any touch down happen or not</returns>
		public bool IsAnyTouchDown(out int index)
		{
#if UNITY_EDITOR
			if (Input.GetMouseButtonDown(mouseButtonIndex))
			{
				index = mouseSimulateIndex;
				mouseDeltaTime = Time.realtimeSinceStartup;
				return true;
			}
#endif
			var touches = Input.touches;
			foreach (var touch in touches)
			{
				if (touch.phase == TouchPhase.Began)
				{
					index = touch.fingerId;
					return true;
				}
			}
			index = nullFingerIndex;
			return false;
		}

		/// <summary>
		/// Check if there is any finger exist
		/// </summary>
		/// <param name="index">the touch index</param>
		/// <returns>any touch happen or not</returns>
		public bool IsAnyTouch(out int index)
		{
#if UNITY_EDITOR
			if (Input.GetMouseButton(mouseButtonIndex))
			{
				index = mouseSimulateIndex;
				return true;
			}
#endif
			var touches = Input.touches;
			foreach (var touch in touches)
			{
				if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
				{
					index = touch.fingerId;
					return true;
				}
			}
			index = nullFingerIndex;
			return false;
		}

		/// <summary>
		/// Check if there is any finger just exit
		/// </summary>
		/// <param name="index">the touch index</param>
		/// <returns>any touch up happen or not</returns>
		public bool IsAnyTouchUp(out int index)
		{
#if UNITY_EDITOR
			if (Input.GetMouseButtonUp(mouseButtonIndex))
			{
				index = mouseSimulateIndex;
				return true;
			}
#endif
			var touches = Input.touches;
			foreach (var touch in touches)
			{
				if (touch.phase == TouchPhase.Ended)
				{
					index = touch.fingerId;
					return true;
				}
			}
			index = nullFingerIndex;
			return false;
		}

		/// <summary>
		/// Check if there is any finger tap
		/// if there is finger hold and doen't move unitl it just up 
		/// it will also be considered as a tap action
		/// </summary>
		/// <param name="index">the touch index</param>
		/// <returns>any tap happen or not</returns>
		public bool IsAnyTouchTap(out int index)
		{
#if UNITY_EDITOR
			if (Input.GetMouseButtonUp(mouseButtonIndex))
			{
				var offset = Time.realtimeSinceStartup - mouseDeltaTime;
				var isTap = offset <= mouseTapThreshold;
				index = mouseSimulateIndex;
				return isTap;
			}
#endif
			var touches = Input.touches;
			foreach (var touch in touches)
			{
				if (touch.phase == TouchPhase.Ended)
				{
					index = touch.fingerId;
					var isTap = touch.tapCount > 0 && !isFingerAlreadyMoved;
					if (isTap)
					{
						return true;
					}
				}
			}
			index = nullFingerIndex;
			return false;
		}

		/// <summary>
		/// Get the screen position of touch
		/// </summary>
		/// <param name="index">the touch index</param>
		/// <returns>the position in screen coordinate</returns>
		public Vector3 GetCurrentPosition(int index = normalTouchIndex)
		{
#if UNITY_EDITOR
			if (!Input.touchSupported)
			{

				return Input.mousePosition;
			}
#endif
			return Input.GetTouch(index).position;
		}

		/// <summary>
		/// Get the details of touch
		/// </summary>
		/// <param name="index">the touch index</param>
		/// <returns>the detail of specific touch</returns>
		public Touch GetTouch(int index)
		{

			if (index == mouseSimulateIndex)
			{
				var result = new Touch();
				result.position = Input.mousePosition;
				return result;
			}
			return Input.GetTouch(index);
		}
	}
}