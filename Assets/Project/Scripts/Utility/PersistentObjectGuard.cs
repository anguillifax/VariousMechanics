using System;
using System.Collections.Generic;
using UnityEngine;

namespace VariousMechanics
{
	/// <summary>
	/// Gracefully handles marking an object as <see
	/// cref="UnityEngine.Object.DontDestroyOnLoad(UnityEngine.Object)"/> without
	/// creating duplicates on scene reload.
	///
	/// <para>
	/// This class relies on the behavior that setting a gameobject to inactive
	/// during Awake() will prevent further invocations of Awake() during the same
	/// frame. Customization of this ability is available in the <see
	/// cref="PersistentObjectKey"/> scriptable object.
	/// </para>
	///
	/// <para>
	/// This component will automatically unparent its attached gameobject as
	/// required by <see
	/// cref="UnityEngine.Object.DontDestroyOnLoad(UnityEngine.Object)"/> to create
	/// a persistent object.
	/// </para>
	/// </summary>
	public class PersistentObjectGuard : MonoBehaviour
	{
		private static readonly List<PersistentObjectKey> allKeys = new();

		public PersistentObjectKey key;

		public bool Active { get; private set; } = false;

		private void Awake()
		{
			if (!key)
			{
				Debug.LogError("No key assigned for persistent object, script requires key to execute", this);
				return;
			}

			if (allKeys.Contains(key))
			{
				switch (key.duplicationAction)
				{
					case PersistentObjectKey.DuplicationAction.DestroySelf:
						gameObject.SetActive(false);
						Destroy(gameObject);
						break;

					case PersistentObjectKey.DuplicationAction.DeactivateSelf:
						gameObject.SetActive(false);
						break;

					case PersistentObjectKey.DuplicationAction.UpdateBooleanOnly:
						// No action, Active boolean is set to false by default
						break;
				}
				return;
			}

			allKeys.Add(key);
			transform.parent = null;
			DontDestroyOnLoad(gameObject);
			Active = true;
		}

		public static bool CanContinue(PersistentObjectGuard guard)
		{
			return guard && guard.Active;
		}
	}
}