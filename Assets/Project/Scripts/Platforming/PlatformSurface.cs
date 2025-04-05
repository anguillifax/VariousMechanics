using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VariousMechanics
{
	public class PlatformSurface : MonoBehaviour
	{
		public bool PlayerAttached
		{
			get => Player;
		}

		public void Attach(PlatformerRigidbody player)
		{
			Player = player;
			Attached.Invoke();
		}

		public void Detach()
		{
			if (Player)
			{
				Player = null;
				Detached.Invoke();
			}
		}

		public PlatformerRigidbody Player;
		public UnityEvent Attached;
		public UnityEvent Detached;
	}
}