using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VariousMechanics
{
	[Serializable]
	public class UniqueId : IEquatable<UniqueId>
	{
		// =========================================================
		// Data
		// =========================================================

		public int A;
		public int B;
		public int C;
		public int D;

		// =========================================================
		// Constructor
		// =========================================================

		public UniqueId()
		{
			Unpack(Guid.Empty);
		}

		public UniqueId(int a, int b, int c, int d)
		{
			A = a;
			B = b;
			C = c;
			D = d;
		}

		public UniqueId(Guid originalValue)
		{
			Unpack(originalValue);
		}

		public UniqueId(BinaryReader reader)
		{
			A = reader.ReadInt32();
			B = reader.ReadInt32();
			C = reader.ReadInt32();
			D = reader.ReadInt32();
		}

		public UniqueId(UniqueId source)
		{
			A = source.A;
			B = source.B;
			C = source.C;
			D = source.D;
		}

		public static UniqueId Empty
		{
			get
			{
				return new UniqueId(Guid.Empty);
			}
		}

		// =========================================================
		// Conversions
		// =========================================================

		private void Unpack(Guid guid)
		{
			byte[] raw = guid.ToByteArray();
			A = BitConverter.ToInt32(raw, 0);
			B = BitConverter.ToInt32(raw, 4);
			C = BitConverter.ToInt32(raw, 8);
			D = BitConverter.ToInt32(raw, 12);
		}

		// =========================================================
		// Operators
		// =========================================================

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (obj is UniqueId otherId)
			{
				return Equals(otherId);
			}
			else
			{
				return false;
			}
		}

		public bool Equals(UniqueId other)
		{
			return this == other;
		}

		public static bool operator ==(UniqueId a, UniqueId b)
		{
			return a is not null && b is not null
				&& a.A == b.A
				&& a.B == b.B
				&& a.C == b.C
				&& a.D == b.D;
		}

		public static bool operator !=(UniqueId a, UniqueId b)
		{
			return !(a == b);
		}

		public bool Exists => this != Empty;

		// =========================================================
		// Serialization
		// =========================================================

		public void Write(BinaryWriter writer)
		{
			writer.Write(A);
			writer.Write(B);
			writer.Write(C);
			writer.Write(D);
		}

		// =========================================================
		// Utility
		// =========================================================

		public override int GetHashCode()
		{
			return HashCode.Combine(A.GetHashCode(), B.GetHashCode(), C.GetHashCode(), D.GetHashCode());
		}

		public override string ToString()
		{
			return $"UniqueId({A} {B} {C} {D})";
		}
	}
}