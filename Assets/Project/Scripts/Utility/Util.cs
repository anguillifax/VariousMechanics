using System;
using System.Collections.Generic;
using UnityEngine;

namespace VariousMechanics
{
	public static class Util
	{
		/// <summary>
		/// Creates a new opaque color from RGB 0-255 values.
		/// </summary>
		public static Color FromRgb256(int r, int g, int b)
		{
			return new Color(r / 256f, g / 256f, b / 256f);
		}

		/// <summary>
		/// Quits the game in a live build and exits editor playmode in the editor.
		/// </summary>
		public static void CloseGameOrExitPlaymode()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
		}

		/// <summary>
		/// Destroys all child gameobjects within a transform.
		/// </summary>
		public static void DestroyAllChildObjects(Transform transform)
		{
			var childObjects = new List<GameObject>();
			for (int i = 0; i < transform.childCount; ++i)
			{
				childObjects.Add(transform.GetChild(i).gameObject);
			}
			childObjects.ForEach(x => UnityEngine.Object.Destroy(x));
		}
	}
}