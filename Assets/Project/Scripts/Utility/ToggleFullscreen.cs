using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VariousMechanics
{
	public class ToggleFullscreen : MonoBehaviour
	{
		private void Update()
		{
			if (Keyboard.current.f11Key.wasPressedThisFrame)
			{
				Screen.fullScreen = !Screen.fullScreen;
			}
		}
	}
}