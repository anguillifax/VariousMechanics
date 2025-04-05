using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VariousMechanics
{
	public class DataManager : MonoBehaviour
	{
		// =========================================================
		// Properties
		// =========================================================

		public static DataManager Instance
		{
			get;
			private set;
		}

		public Archive CurrentSave { get; private set; }

		// =========================================================
		// Variables
		// =========================================================

		public bool logInfo;

		// =========================================================
		// Callbacks
		// =========================================================

		private void Awake()
		{
			Instance = this;
			CurrentSave = new Archive();
		}

		// =========================================================
		// Public API
		// =========================================================

		public void CreateEmptySave()
		{
			CurrentSave = new Archive();
			if (logInfo) Log("Created empty archive");
		}

		public void LoadFromDisk(string fileName)
		{
			string path = GetPath(fileName);
			if (!File.Exists(path))
			{
				if (logInfo) Log($"No archive at path `{fileName}`, creating blank save");
				CreateEmptySave();
				return;
			}

			using BinaryReader reader = new(File.OpenRead(path));
			CurrentSave = new Archive(reader);

			if (logInfo) Log($"Finished parsing archive at path `{fileName}`");
		}

		public void RequestDataFromWorldEntities()
		{
			var all = FindObjectsByType<SaveSerialize>(FindObjectsInactive.Include, FindObjectsSortMode.None);

			foreach (var item in all)
			{
				item.RequestSave();
			}

			if (logInfo) Log($"Finished requesting save data from {all.Length} objects");
		}

		public void WriteToDisk(string fileName)
		{
			string path = GetPath(fileName);
			using BinaryWriter writer = new(File.OpenWrite(path));
			CurrentSave.WriteToFile(writer);

			if (logInfo) Log($"Finished writing archive to `{fileName}` on disk");
		}

		public void EraseFromDisk(string fileName)
		{
			string path = GetPath(fileName);
			string backupPath = GetPath(fileName + ".bak");
			File.Delete(backupPath);
			File.Move(path, backupPath);
			if (logInfo) Log($"Deleted save `{fileName}` off of disk");
		}

		// =========================================================
		// Utility
		// =========================================================

		private string GetPath(string fileName)
		{
			return Path.Combine(Application.persistentDataPath, fileName);
		}

		private void Log(object contents)
		{
			Debug.Log($"[DataManager] {contents}", this);
		}
	}
}