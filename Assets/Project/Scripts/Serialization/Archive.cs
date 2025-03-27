using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VariousMechanics
{
	public class Archive
	{
		// =========================================================
		// Read Only Record
		// =========================================================

		public class ReadOnlyRecord : IDisposable
		{
			private readonly BinaryReader reader;

			public ReadOnlyRecord(byte[] data)
			{
				reader = new BinaryReader(new MemoryStream(data));
			}

			public int ReadInt() => reader.ReadInt32();
			public bool ReadBoolean() => reader.ReadBoolean();

			public void Dispose()
			{
				reader.Dispose();
			}
		}

		// =========================================================
		// Write Only Record
		// =========================================================

		public class WriteOnlyRecord : IDisposable
		{
			private readonly BinaryWriter writer;
			private readonly MemoryStream stream;

			public WriteOnlyRecord()
			{
				stream = new MemoryStream();
				writer = new BinaryWriter(stream);
			}

			public void Write(int value) => writer.Write(value);
			public void Write(bool value) => writer.Write(value);

			public byte[] ToBytes()
			{
				return stream.ToArray();
			}

			public void Dispose()
			{
				writer.Dispose();
				stream.Dispose();
			}
		}

		// =========================================================
		// Magic Constants
		// =========================================================

		private const string MagicHeader = "GAMESAVE";
		private const string MagicFooter = "ENDOFSAVEDATA";
		private const int CurrentVersion = 1;

		// =========================================================
		// Data
		// =========================================================

		private readonly Dictionary<UniqueId, byte[]> library;

		// =========================================================
		// Constructor
		// =========================================================

		public Archive()
		{
			library = new Dictionary<UniqueId, byte[]>();
		}

		public Archive(BinaryReader reader)
		{
			string header = new(reader.ReadChars(MagicHeader.Length));
			if (header != MagicHeader)
			{
				throw new Exception("Source data is not a save file");
			}

			int version = reader.ReadInt32();
			if (version != CurrentVersion)
			{
				throw new Exception($"Version mismatch, expected {CurrentVersion} but got {version}");
			}

			int recordCount = reader.ReadInt32();
			library = new Dictionary<UniqueId, byte[]>(recordCount);

			for (int i = 0; i < recordCount; i++)
			{
				UniqueId id = new(reader);
				int length = reader.ReadInt32();
				byte[] record = reader.ReadBytes(length);
				library.Add(id, record);
			}

			string footer = new(reader.ReadChars(MagicFooter.Length));
			if (footer != MagicFooter)
			{
				throw new Exception("Invalid footer");
			}
		}

		// =========================================================
		// Read Access
		// =========================================================

		public bool TryLoad(UniqueId id, out ReadOnlyRecord record)
		{
			if (library.TryGetValue(id, out byte[] data))
			{
				record = new ReadOnlyRecord(data);
				return true;
			}

			record = null;
			return false;
		}

		// =========================================================
		// Write Access
		// =========================================================

		public WriteOnlyRecord BlankRecord => new WriteOnlyRecord();

		public void Save(UniqueId id, WriteOnlyRecord record)
		{
			byte[] data = record.ToBytes();
			library.Remove(id);
			library.Add(id, data);
		}

		// =========================================================
		// Serialization
		// =========================================================

		public void WriteToFile(BinaryWriter writer)
		{
			writer.Write(MagicHeader.ToCharArray());
			writer.Write(CurrentVersion);
			writer.Write(library.Count);

			foreach (var (id, data) in library)
			{
				id.Write(writer);
				writer.Write(data.Length);
				writer.Write(data);
			}

			writer.Write(MagicFooter.ToCharArray());
		}
	}
}