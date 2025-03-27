using System;
using System.Collections.Generic;
using UnityEngine;

namespace VariousMechanics
{
	public class RemoveOnPlay : MonoBehaviour
	{
		public enum RemovalMode
		{
			DestroySelf, DeactivateSelf,
		}

		public RemovalMode mode = RemovalMode.DestroySelf;

		public void Awake()
		{
			switch (mode)
			{
				case RemovalMode.DestroySelf:
					// If the object is set to Inactive during Awake() it will
					// prevent other Awake() functions from executing
					gameObject.SetActive(false);
					Destroy(gameObject);
					break;

				case RemovalMode.DeactivateSelf:
					gameObject.SetActive(false);
					break;
			}
		}
	}
}