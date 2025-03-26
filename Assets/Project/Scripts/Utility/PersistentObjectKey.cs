using System;
using System.Collections.Generic;
using UnityEngine;

namespace VariousMechanics
{
	[CreateAssetMenu(menuName = "Various Mechanics/Persistent Object Key")]
	public class PersistentObjectKey : ScriptableObject
	{
		public enum DuplicationAction
		{
			DestroySelf,
			DeactivateSelf,
			UpdateBooleanOnly,
		}

		public DuplicationAction duplicationAction;
	}
}