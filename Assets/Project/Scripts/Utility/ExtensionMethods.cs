using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VariousMechanics
{
	public static class ExtensionMethods
	{
		/// <summary>
		/// Changes a game object's active state only if it is different than the current state.
		/// </summary>
		public static void SetActiveLazy(this GameObject target, bool active)
		{
			if (active != target.activeSelf)
			{
				target.SetActive(active);
			}
		}

		/// <summary>
		/// Overwrites the opacity of a color.
		/// </summary>
		public static Color WithAlpha(this Color color, float newAlpha)
		{
			color.a = newAlpha;
			return color;
		}
	}
}