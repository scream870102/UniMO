using UnityEngine;
using UnityEngine.UI;

namespace Scream.UniMO.Utils
{
	public static class UnityUIExtension
	{

		/// <summary>
		/// Set value of a image component and will ignore when component doesn't exist.
		/// </summary>
		/// <param name="sprite">the sprite you want to set to this image.null is valid value</param>
		public static void SetImg(this Image image, Sprite sprite)
		{
			if (image == null)
				return;
			image.sprite = sprite;
		}

		/// <summary>
		/// Set value of a text component and will ignore when component doesn't exist.
		/// </summary>
		/// <param name="context">the value you want to set</param>
		public static void SetText<T>(this Text text, T context)
		{
			if (text == null)
				return;
			text.text = context.ToString();
		}
	}
}