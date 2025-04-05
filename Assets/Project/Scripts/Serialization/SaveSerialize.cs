using System;
using System.Collections.Generic;
using UnityEngine;

namespace VariousMechanics
{
	public abstract class SaveSerialize : MonoBehaviour
	{
		public UniqueId UniqueId;

		private void OnValidate()
		{
			if (!UniqueId.Exists)
			{
				UniqueId = new UniqueId(Guid.NewGuid());
				Debug.Log("Populated Guid", this);
			}
		}

		public void RequestLoad()
		{
			Archive ar = DataManager.Instance.CurrentSave;
			if (ar.TryLoad(UniqueId, out var readOnlyRecord))
			{
				LoadState(readOnlyRecord);
			}
		}

		public void RequestSave()
		{
			Archive ar = DataManager.Instance.CurrentSave;
			var record = ar.BlankRecord;
			WriteState(record);
			ar.Save(UniqueId, record);
		}

		public abstract void LoadState(Archive.ReadOnlyRecord ar);
		public abstract void WriteState(Archive.WriteOnlyRecord ar);
	}
}