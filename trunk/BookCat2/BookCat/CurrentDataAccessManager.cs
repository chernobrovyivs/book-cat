using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using FluidKit.Controls;
using System.Data.EntityClient;
using System.Data;
using System.IO;
using Ookii.Dialogs.Wpf;
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;

namespace BookCat
{
	public class CurrentDataAccessManager
	{
        public static readonly CurrentDataAccessManager current = new CurrentDataAccessManager();


		CatDbEntities c = new CatDbEntities();

        //public void Save()
        //{
        //    c.SaveChanges(true);
        //}

        public void Undo(object entity)
        {
            c.Refresh(RefreshMode.StoreWins, entity);
        }

		private ObservableCollection<Book> allBooks;
		public ObservableCollection<Book> getAllBooks()
		{
			if (allBooks==null)
			{
			    allBooks = new ObservableCollection<Book>(c.Book);
/*
                allBooks = new ObservableCollection<Book>();
                allBooks.Add(new Book() { Book_guid = Guid.NewGuid(), Name = "Расрас" });
				allBooks.Add(new Book(){Book_guid = Guid.NewGuid(),Name = "Двадва"});
				allBooks.Add(new Book(){Book_guid = Guid.NewGuid(),Name = "Тритри"});
				allBooks.Add(new Book(){Book_guid = Guid.NewGuid(),Name = "Четыре"});
				allBooks.Add(new Book(){Book_guid = Guid.NewGuid(),Name = "Пятьпять"});
*/ 
			}

			return allBooks;
		}
        public ObservableCollection<Book> getNewBooks()
        {
            //string query = "SELECT Book_guid FROM Book ORDER BY Book_guid";

            //var products = c.CreateQuery<Book>(query, new[] { new ObjectParameter("customerId", customerId) }).ToList();
            //var p = c.CreateQuery<Book>(query).ToList();

            //return new ObservableCollection<Book>(p);


            //var k = c.Book.Top("1");


            //var kk = from cc in c.Book 
                     //orderby 

            var z =
                    from n in c.Book
                    orderby n.Dt_added, n.Name
                    select n;


            // c.Book.OrderBy("it.Name")
            return new ObservableCollection<Book>(z);

            ObservableCollection<Book> bc = new ObservableCollection<Book>();

            foreach (Book b in c.Book)
            {
                bc.Add(b);
            }

            return bc;
        }


        private ObservableCollection<CGenre> allGenres;
        public ObservableCollection<CGenre> getAllGenres()
        {
            if (allGenres == null)
            {
                var oc = new ObservableCollection<CGenre>();

                foreach (Genre g in c.Genre)
                {
                    oc.Add(new CGenre()
                    {
                        Name = g.Name,
                        About = g.About,
                        Genre_guid = g.Genre_guid,
                        Top_guid = g.Top_guid,
                        parent = oc
                    }
                    );
                }

                allGenres = oc;
            }

            return allGenres;
        }


		public bool AddToBook(Book _book)
		{
			_book.Book_guid = Guid.NewGuid();

			c.AddToBook(_book);

			return true;
		}

		public int SaveChanges()
		{
			return c.SaveChanges();
			//return 0;
		}

	    public void AddToGenre(CGenre ci)
	    {
	        Genre g = new Genre();
	        g.Name = ci.Name;
	        g.About = ci.About;
	        g.Genre_guid = ci.Genre_guid;
	        g.Top_guid = ci.Top_guid;

	        c.AddToGenre(g);

            c.SaveChanges();
	    }

	    public void UpdateCGenre(CGenre cur)
	    {

	        Genre gg = c.Genre.First((g) => g.Genre_guid == cur.Genre_guid);

	        gg.Name = cur.Name;
	        gg.About = cur.About;
	        gg.Top_guid = cur.Top_guid;

	        c.SaveChanges();
	        
	    }

        public ObservableCollection<Book> getBooksByGenreGuid(Guid _Genre_guid)
        {
            var z = from n in c.Book
                    where n.Genre_Guid == _Genre_guid
                    select n;

            return new ObservableCollection<Book>(z);
        }

	    public void updateBookGenre(Guid curGenreGuid, List<Guid> arr)
	    {
            foreach(Guid gu in arr)
            {
                var b = from n in c.Book
                        where n.Book_guid == gu
                        select n;

                Book bok = b.ToArray()[0];

                bok.Genre_Guid = curGenreGuid;
            }

	        c.SaveChanges();
	    }
	}
}
