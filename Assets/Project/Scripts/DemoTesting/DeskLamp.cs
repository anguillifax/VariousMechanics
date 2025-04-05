using System;
using System.Collections.Generic;
using UnityEngine;

namespace VariousMechanics
{
	public class DeskLamp : SaveSerialize
	{
		public bool Active;
		public Material MatActive;
		public Material MatInactive;

		private MeshRenderer mr;

		public void Awake()
		{
			RequestLoad();

			mr = GetComponent<MeshRenderer>();
		}

		private void Update()
		{
			mr.sharedMaterial = Active ? MatActive : MatInactive;
		}

		public override void LoadState(Archive.ReadOnlyRecord ar)
		{
			Active = ar.ReadBoolean();
		}

		public override void WriteState(Archive.WriteOnlyRecord ar)
		{
			ar.Write(Active);
		}
	}
}