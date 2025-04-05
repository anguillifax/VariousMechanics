using System;
using System.Collections.Generic;
using UnityEngine;

namespace VariousMechanics
{
	public class ColorizedPlatformSurface : MonoBehaviour
	{
		private void Awake()
		{
			if (!Surface)
			{
				Debug.LogError("Missing platform surface", this);
				enabled = false;
				return;
			}

			Surface.Attached.AddListener(OnAttached);
			Surface.Detached.AddListener(OnDetached);
			Renderer.sharedMaterial = Surface.PlayerAttached ? ActiveMaterial : InactiveMaterial;
		}

		public void OnAttached()
		{
			Renderer.sharedMaterial = ActiveMaterial;
		}

		public void OnDetached()
		{
			Renderer.sharedMaterial = InactiveMaterial;
		}

		public PlatformSurface Surface;
		public MeshRenderer Renderer;
		public Material InactiveMaterial;
		public Material ActiveMaterial;
	}
}