using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookCat
{
	public class EntityForDumbComputer
	{
		static EntityForDumbComputer()
		{

			Boo = new List<Book>();

			Boo.Add(new Book() { Book_guid = Guid.NewGuid(), Name = "fsdfsdsfff" });
			Boo.Add(new Book() { Book_guid = Guid.NewGuid(), Name = "fsdfsdsfff" });
			Boo.Add(new Book() { Book_guid = Guid.NewGuid(), Name = "fsdfsdsfff" });
		
		}

		public static List<Book> Boo{
			get;
			private set;}
	}
}
