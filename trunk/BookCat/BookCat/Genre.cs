using System;
using System.Collections.Generic;
using System.Text;

namespace BookCat
{
	public class Genre
	{
		public long Top_id;
		public long BookCat_id;
		public string Name;

		public static readonly Genre Empty = new Genre();

		public string GetHeader
		{
			get
			{
				if (IsEmpty) return "Неизвестный жанр";
				return Name;
			}
		}

		public bool IsEmpty
		{
			get
			{
				if (BookCat_id == 0) return true;
				return false;
			}
		}

		public override bool Equals(Object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Genre)) return false;
			return Equals((Genre) obj);
		}

		public bool Equals(Genre other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Top_id == Top_id && other.BookCat_id == BookCat_id && Equals(other.Name, Name);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = Top_id.GetHashCode();
				result = (result*397) ^ BookCat_id.GetHashCode();
				result = (result*397) ^ (Name != null ? Name.GetHashCode() : 0);
				return result;
			}
		}
	}
}
