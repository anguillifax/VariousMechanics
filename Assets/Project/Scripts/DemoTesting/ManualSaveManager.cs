using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace VariousMechanics
{
	public class ManualSaveManager : MonoBehaviour
	{
		public string FileName = "demo.sav";

		private void Update()
		{
			if (Keyboard.current.digit1Key.wasPressedThisFrame)
			{
				bool shift = Keyboard.current.shiftKey.isPressed;
				bool alt = Keyboard.current.altKey.isPressed;

				if (!shift && !alt)
				{
					// Load from disk
					DataManager.Instance.LoadFromDisk(FileName);
					ApplyData();
				}
				else if (shift && !alt)
				{
					// Write to disk
					DataManager.Instance.RequestDataFromWorldEntities();
					DataManager.Instance.WriteToDisk(FileName);
				}
				else if (!shift && alt)
				{
					// Load clean copy
					DataManager.Instance.CreateEmptySave();
					ApplyData();
				} else if (shift && alt)
				{
					// Factory reset and reload
					DataManager.Instance.EraseFromDisk(FileName);
					DataManager.Instance.CreateEmptySave();
					ApplyData();
				}
			}
		}

		private void ApplyData()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}