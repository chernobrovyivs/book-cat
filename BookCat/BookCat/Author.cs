namespace BookCat
{
	public class Author
	{
		public static readonly Author Empty = new Author() {Fio = "[ Без автора ]"};

		public bool IsEmpty
		{
			get
			{
				if (Author_id == 0) return true;
				return false;
			}
		}

		public string GetHeader
		{
			get
			{
				if (IsEmpty) return "Неизвестный автор";
				return Fio;
			}
		}

		public long Author_id;

		public string Fio;
		public string Dates;
		public string About;

		public string Cover_filename;

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Author)) return false;
			return Equals((Author) obj);
		}

		public bool Equals(Author other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Author_id == Author_id && Equals(other.Fio, Fio) && Equals(other.Dates, Dates) && Equals(other.About, About) && Equals(other.Cover_filename, Cover_filename);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = Author_id.GetHashCode();
				result = (result*397) ^ (Fio != null ? Fio.GetHashCode() : 0);
				result = (result*397) ^ (Dates != null ? Dates.GetHashCode() : 0);
				result = (result*397) ^ (About != null ? About.GetHashCode() : 0);
				result = (result*397) ^ (Cover_filename != null ? Cover_filename.GetHashCode() : 0);
				return result;
			}
		}
	}

}
